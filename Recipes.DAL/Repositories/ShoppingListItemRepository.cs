using Recipes.Dal.Repositories;
using Recipes.DAL.Data;
using Recipes.Domain;

namespace Recipes.DAL.Repositories
{
    public class ShoppingListItemRepository : RepositoryBase<ShoppingListItem>
    {
        public ShoppingListItemRepository(DataContext dc) : base(dc)
        {
        }
    }//class
}//ns
