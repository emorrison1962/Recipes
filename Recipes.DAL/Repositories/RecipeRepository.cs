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
	public class RecipeRepository : RepositoryBase<Recipe>
	{
		public RecipeRepository(DataContext dc) : base (dc)
		{

		}

		public override Recipe GetById(int id)
		{
            var query = this._dbSet
                .Where(r => r.RecipeId == id)
                .IncludeMultiple(
                    r => r.IngredientGroups
                    , r => r.IngredientGroups.Select<IngredientGroup, List<IngredientGroupItem>>(pg => pg.Items)
                    , r => r.ProcedureGroups
                    , r => r.ProcedureGroups.Select<ProcedureGroup, List<ProcedureGroupItem>>(pg => pg.Items));
                        
            var result = query.FirstOrDefault();

			//var result = _dbSet.Find(id);
			return result;
		}

	}
}
