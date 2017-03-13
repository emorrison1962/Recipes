using Recipes.Contracts.Repositories;
using Recipes.Contracts.Services;
using Recipes.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Recipes.Services
{
    public class ShoppingListService : ServiceBase<ShoppingList>, IShoppingListService
    {
        IRepositoryBase<ShoppingList> Repository { get; set; }
        IServiceBase<IngredientGroupItem> IngredientGroupItemService { get; set; }

        public ShoppingListService(IRepositoryBase<ShoppingList> repository, IServiceBase<IngredientGroupItem> ingredientGroupItemService)
            : base(repository)
        {
            this.Repository = repository;
            this.IngredientGroupItemService = ingredientGroupItemService;
        }

        override public IEnumerable<ShoppingList> GetAll()
        {
            List<ShoppingList> result;
            try
            {
#warning We're only supporting one global list right now.
                result = this.Repository.GetAll().ToList();
                result.Sort();
            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.ToString());
                throw;
            }
            return result;
        }

        override public ShoppingList Insert(ShoppingList entity)
        {
            try
            {
                this.Repository.Insert(entity);
                this.Repository.Commit();
            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.ToString());
                throw;
            }

            return entity;
        }

        override public ShoppingList Update(ShoppingList entity)
        {
            try
            {
                this.Repository.Update(entity);
                this.Repository.Commit();
            }
#pragma warning disable 168
            catch (Exception ex)
            {
                throw;
            }
#pragma warning restore 168

            return entity;
        }

        public bool Update(int id, List<int> items)
        {
            var result = false;

            var existing = this.GetById(id);

            var inserts = (
                from itemId in items
                where !(from existingItem in existing.Items select existingItem.IngredientGroupItemId)
                  .Contains(itemId)
                select itemId).ToList();
            foreach (var insert in inserts)
            {
                var existingIgi = this.IngredientGroupItemService.GetById(insert);
                existing.Items.Add(existingIgi);
            }


            var deletes = (
                from existingItem in existing.Items
                where !(from itemId in items select itemId)
                  .Contains(existingItem.IngredientGroupItemId)
                select existingItem).ToList();
            deletes.ForEach(x => existing.Items.Remove(x));

            try
            {
                this.Repository.Update(existing);
                this.Repository.Commit();
                result = true;
            }
#pragma warning disable 168
            catch (Exception ex)
            {
                throw;
            }
#pragma warning restore 168

            return result;
        }

    }//class
}//ns
