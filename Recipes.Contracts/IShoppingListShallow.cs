using System.Collections.Generic;

namespace Recipes.Contracts
{
    public interface IShoppingListShallow
    {
        List<int> Items { get; set; }
    }
}