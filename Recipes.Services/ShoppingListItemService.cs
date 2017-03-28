using Recipes.Contracts.Repositories;
using Recipes.Contracts.Services;
using Recipes.Domain;

namespace Recipes.Services
{
    public class ShoppingListItemService : ServiceBase<ShoppingListItem>, IServiceBase<ShoppingListItem>
    {
        public ShoppingListItemService(IRepositoryBase<ShoppingListItem> repository)
            : base(repository)
        {
        }

    }//class
}
