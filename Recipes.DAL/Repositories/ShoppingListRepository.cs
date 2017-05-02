using Recipes.Dal.Repositories;
using Recipes.DAL.Data;
using Recipes.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;

namespace Recipes.DAL.Repositories
{
    public class ShoppingListRepository : RepositoryBase<ShoppingList>
    {
        public ShoppingListRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public override ShoppingList GetFullObject(object id)
        {

#if false
            var query = this._dbSet
                .Where(r => r.RecipeId == id)
                .IncludeMultiple(
                    r => r.IngredientGroups
                    , r => r.IngredientGroups.Select<IngredientGroup, List<IngredientItem>>(pg => pg.Items)
                    , r => r.ProcedureGroups
                    , r => r.ProcedureGroups.Select<ProcedureGroup, List<ProcedureItem>>(pg => pg.Items));
                        
            var result = query.FirstOrDefault();
#endif
            var result = this._dbSet
                .Where(l => l.ShoppingListId != int.MinValue)
                .IncludeMultiple(l => l.Groups,
                    l => l.Groups.Select(slg => slg.Items))
                .FirstOrDefault();

            return result;
        }

        class ShoppingListItemIdComparer : IEqualityComparer<ShoppingListItem>
        {
            public bool Equals(ShoppingListItem x, ShoppingListItem y)
            {
                return x.ShoppingListItemId == y.ShoppingListItemId;
            }

            public int GetHashCode(ShoppingListItem obj)
            {
                int result =
                obj.ShoppingListItemId.GetHashCode()
                    ^ obj.Text.GetHashCode();
                return result;
            }
        }

        public override void Update(ShoppingList incoming)
        {
            try
            {
                var existing = this.GetFullObject(incoming.ShoppingListId);
                if (!existing.Equals(incoming))
                {
                    var ar = existing.DetectChanges(incoming);
                    _dataContext.SetChanges(ar);
                    _dataContext.SaveChanges();
                }

#if false
                var existing = this.GetFullObject(incoming.ShoppingListId);
                var defaultGroup = existing.Groups[0];

                var existingItems = (
                    from g in existing.Groups
                    from i in g.Items
                    select new { Item = i, GroupId = g.ShoppingListGroupId }).ToList();
                var incomingItems = (
                    from g in incoming.Groups
                    from i in g.Items
                    select new { Item = i, GroupId = g.ShoppingListGroupId }).ToList();

                var itemRepository = new ShoppingListItemRepository(this._dataContext);

                #region Inserts

                var modifiedGroups = new HashSet<ShoppingListGroup>();

                var excludes = existingItems.Select(x => x.Item.ShoppingListItemId);
                var pendingInserts = incomingItems.Where(x => !excludes.Contains(x.Item.ShoppingListItemId)).ToList();
                pendingInserts.ForEach(x => _dataContext.ShoppingListItems.Add(x.Item));
                pendingInserts.ForEach(x => defaultGroup.Add(x.Item));
                if (pendingInserts.Count > 0)
                    modifiedGroups.Add(defaultGroup);


                #endregion

                #region Updates

                var groupRepository = new ShoppingListGroupRepository(this._dataContext);

                var updates = (
                    from e in existingItems
                    from i in incomingItems
                    where e.Item.ShoppingListItemId == i.Item.ShoppingListItemId
                        && (e.Item.IsChecked != i.Item.IsChecked
                            || e.GroupId != i.GroupId)
                    select new { Existing = e, Incoming = i }
                    );

                IEnumerable<ShoppingListGroup> existingGroups = null;
                if (updates.Count() > 0)
                {
                    existingGroups = groupRepository.GetAll();
                }

                foreach (var update in updates)
                {
                    var incomingItem = update.Incoming.Item;
                    var existingItem = update.Existing.Item;
                    if (existingItem.IsChecked != incomingItem.IsChecked)
                    {
                        existingItem.IsChecked = incomingItem.IsChecked;
                    }
                    if (update.Existing.GroupId != update.Incoming.GroupId)
                    {
                        var group = existingGroups.Where(x => x.ShoppingListGroupId == update.Incoming.GroupId).First();
                        modifiedGroups.Add(existingItem.ShoppingListGroup);
                        modifiedGroups.Add(group);
                        existingItem.ShoppingListGroup.Remove(existingItem);

                        existingItem.ShoppingListGroup = group;
                        group.Add(existingItem);
                    }
                }

                #endregion

                modifiedGroups.ToList().ForEach(x => _dataContext.Entry(x).State = EntityState.Modified);

                _dataContext.SaveChanges();

#endif
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                throw;
            }
#pragma warning disable 0168
            catch (Exception ex)
            {
                throw;
            }
#pragma warning restore 0168
        }

    }//class
}//ns
