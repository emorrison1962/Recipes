using Recipes.Contracts.Repositories;
using Recipes.Contracts.Services;
using Recipes.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Recipes.Services
{
    public class ShoppingListService : ServiceBase<ShoppingList>, IServiceBase<ShoppingList>
    {
        #region Fields


        #endregion

        #region Properties

        IServiceBase<ShoppingListGroup> ShoppingListGroupService { get; set; }

        #endregion

        #region Construction

        public ShoppingListService(IRepositoryBase<ShoppingList> repository,
            IServiceBase<ShoppingListGroup> shoppingListGroupService)
            : base(repository)
        {
            this.ShoppingListGroupService = shoppingListGroupService;
        }

        #endregion

        override public IEnumerable<ShoppingList> GetAll()
        {
            List<ShoppingList> result;
            try
            {
                result = this.Repository.GetAll().ToList();
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

        class ShoppingListItemIdComparer : IEqualityComparer<ShoppingListItem>
        {
            public bool Equals(ShoppingListItem x, ShoppingListItem y)
            {
                return x.ShoppingListItemId == y.ShoppingListItemId;
            }

            public int GetHashCode(ShoppingListItem obj)
            {
                return obj.ShoppingListItemId.GetHashCode();
            }
        }

        public bool Update(int id, List<ShoppingListItem> incomingItems)
        {
            var result = false;
            var existing = this.GetFullObject(id);
            var defaultGroup = existing.Groups[0];

            var existingItems = (
                from g in existing.Groups
                from i in g.Items
                select (i)).ToList();

            var pendingInserts = incomingItems.Except(existingItems, new ShoppingListItemIdComparer()).ToList();
            var pendingDeletes = existingItems.Except(incomingItems, new ShoppingListItemIdComparer()).ToList();
            


            /*
            var newManualEntries = incomingItems.Where(x => x.ShoppingListItemId == 0).ToList();
            if (newManualEntries.Count > 0)
            {//manually entered IngredientItems.
                foreach (var me in newManualEntries)
                {
                    me.ShoppingListGroup = defaultGroup;
                    defaultGroup.Items.Add(me);

                    ShoppingListGroupService.Update(defaultGroup);
                    existing.Add(me);
                    incomingItems.Remove(me);
                }
            }

            var existingIds = existing.Items.Select(x => x.IngredientItemId).ToList();
            var incomingIds = incomingItems.Select(x => x.IngredientItemId).ToList();

            var insertIds = incomingIds.Except(existingIds);
            foreach (var insert in insertIds)
            {
                var existingIgi = this.IngredientItemService.GetById(insert);
                existingIgi = this.IngredientItemService.Detach(existingIgi);
                existing.Items.Add(existingIgi);
            }


            var deleteIds = existingIds.Except(incomingIds).ToList();
            var deletes = existing.Items.Where(x => deleteIds.Contains(x.IngredientItemId)).Select(x => x).ToList();
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
*/
            return result;
        }

    }//class
}//ns
