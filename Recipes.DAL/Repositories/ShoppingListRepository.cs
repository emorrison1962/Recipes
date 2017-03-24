using Recipes.Dal.Repositories;
using Recipes.DAL.Data;
using Recipes.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    ^obj.Text.GetHashCode();
                return result;
            }
        }

        public override void Update(ShoppingList incoming)
        {
            try
            {
                var existing = this.GetFullObject(incoming.ShoppingListId);
                var defaultGroup = existing.Groups[0];

                var existingItems = (
                    from g in existing.Groups
                    from i in g.Items
                    select (i)).ToList();
                var incomingItems = (
                    from g in incoming.Groups
                    from i in g.Items
                    select (i)).ToList();

                var itemRepository = new ShoppingListItemRepository(this._dataContext);

                var modifiedGroups = new HashSet<ShoppingListGroup>();
                var pendingInserts = incomingItems.Except(existingItems, new ShoppingListItemIdComparer()).ToList();
                pendingInserts.ForEach(x => _dataContext.ShoppingListItems.Add(x));
                pendingInserts.ForEach(x => defaultGroup.Add(x));
                if (pendingInserts.Count > 0)
                    modifiedGroups.Add(defaultGroup);

                var pendingDeletes = existingItems.Except(incomingItems, new ShoppingListItemIdComparer()).ToList();

                pendingDeletes.OrderBy(x => x.ShoppingListGroup.ShoppingListGroupId);
                foreach (var item in pendingDeletes)
                {
                    modifiedGroups.Add(item.ShoppingListGroup);
                    item.ShoppingListGroup.Items.Remove(item);
                }

                var groupRepository = new ShoppingListGroupRepository(this._dataContext);
                modifiedGroups.ToList().ForEach(x => _dataContext.Entry(x).State = EntityState.Modified);
                _dataContext.SaveChanges();
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
