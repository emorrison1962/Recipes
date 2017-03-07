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
			var result = _dbSet
				.Include("IngredientGroups.Items")
				.Include("ProcedureGroups.Items")
				.Where(r => r.RecipeId == id).FirstOrDefault();
			//var result = _dbSet.Find(id);
			return result;
		}

	}
}
