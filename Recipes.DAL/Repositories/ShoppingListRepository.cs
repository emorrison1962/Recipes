using Recipes.Dal.Repositories;
using Recipes.DAL.Data;
using Recipes.Domain;
using System;
using System.Collections.Generic;
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
                .Where(l => l.ShoppingListId == (int)id)
                .IncludeMultiple(l => l.Groups,
                    l => l.Groups.Select(slg => slg.Items))
                .FirstOrDefault();

            return result;
        }

    }//class
}//ns
