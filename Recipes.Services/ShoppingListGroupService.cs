using Recipes.Contracts.Repositories;
using Recipes.Contracts.Services;
using Recipes.Domain;

namespace Recipes.Services
{
    public class ShoppingListGroupService : ServiceBase<ShoppingListGroup>, IServiceBase<ShoppingListGroup>
    {
        public ShoppingListGroupService(IRepositoryBase<ShoppingListGroup> repository)
            : base(repository)
        {
        }

    }//class
}//ns
