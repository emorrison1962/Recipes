using Recipes.Dal.Repositories;
using Recipes.DAL.Data;
using Recipes.Domain;

namespace Recipes.DAL.Repositories
{
    public class IngredientItemRepository : RepositoryBase<IngredientItem>
    {
        public IngredientItemRepository(DataContext dc) : base(dc)
        {
        }
    }
}
