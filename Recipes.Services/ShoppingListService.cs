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

        public bool Update(int id, List<IngredientGroupItem> incomingItems)
        {
            var result = false;

            var sl = this.GetFullObject(id);

            var existingIds = sl.Items.Select(x => x.IngredientGroupItemId).ToList();
            var incomingIds = incomingItems.Select(x => x.IngredientGroupItemId).ToList();

            var insertIds = incomingIds.Except(existingIds);
            foreach (var insert in insertIds)
            {
                var existingIgi = this.IngredientGroupItemService.GetById(insert);
                existingIgi = this.IngredientGroupItemService.Detach(existingIgi);
                sl.Items.Add(existingIgi);
            }


            var deleteIds = existingIds.Except(incomingIds).ToList();
            var deletes = sl.Items.Where(x => deleteIds.Contains(x.IngredientGroupItemId)).Select(x => x).ToList();
            deletes.ForEach(x => sl.Items.Remove(x));

            try
            {
                this.Repository.Update(sl);
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
