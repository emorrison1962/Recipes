using Recipes.Dal.Repositories;
using Recipes.DAL.Data;
using Recipes.Domain;

namespace Recipes.DAL.Repositories
{
    public class IngredientGroupItemRepository : RepositoryBase<IngredientGroupItem>
    {
        public IngredientGroupItemRepository(DataContext dc) : base(dc)
        {
        }
    }
}
