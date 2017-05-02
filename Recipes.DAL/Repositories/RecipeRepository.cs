using Recipes.Dal.Repositories;
using Recipes.DAL.Data;
using Recipes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Recipes.DAL.Repositories
{
    public class RecipeRepository : RepositoryBase<Recipe>
    {
        public RecipeRepository(DataContext dc) : base(dc)
        {

        }

        public override Recipe GetById(int id)
        {
            var query = this._dbSet
                .Where(r => r.RecipeId == id)
                .IncludeMultiple(
                    r => r.IngredientGroups
                    , r => r.IngredientGroups.Select<IngredientGroup, List<IngredientItem>>(pg => pg.Items.ToList())
                    , r => r.ProcedureGroups
                    , r => r.ProcedureGroups.Select<ProcedureGroup, List<ProcedureItem>>(pg => pg.Items.ToList()));

            var result = query.FirstOrDefault();

            return result;
        }

        public override IEnumerable<Recipe> GetAll(object filter)
        {
            var result = new List<Recipe>();
            if (filter is Func<Recipe, bool>)
            {
                var where = filter as Func<Recipe, bool>;
                result = this._dbSet.AsEnumerable().Where(x => where(x)).ToList();
            }
            else
            {
                throw new ArgumentOutOfRangeException("This filter type is not supported.");
            }
            return result;
        }

    }
}
