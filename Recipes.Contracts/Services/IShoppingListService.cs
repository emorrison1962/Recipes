using Recipes.Domain;
using System.Collections.Generic;

namespace Recipes.Contracts.Services
{
    public interface IShoppingListService : IServiceBase<ShoppingList>
    {
        bool Update(int id, List<IngredientItem> items);
    }
}