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
            var query = this.DbSet
                .Where(r => r.RecipeId == id)
                .IncludeMultiple(
                    r => r.IngredientGroups
                    , r => r.IngredientGroups.Select(pg => pg.Items)
                    , r => r.ProcedureGroups
                    , r => r.ProcedureGroups.Select(pg => pg.Items));

            var result = query.FirstOrDefault();

            return result;
        }

        public override IEnumerable<Recipe> GetAll(object filter)
        {
            var result = new List<Recipe>();
            if (filter is Func<Recipe, bool>)
            {
                var where = filter as Func<Recipe, bool>;
                result = this.DbSet.AsEnumerable().Where(x => where(x)).ToList();
            }
            else
            {
                throw new ArgumentOutOfRangeException("This filter type is not supported.");
            }
            return result;
        }

    }
}
