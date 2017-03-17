using System.Collections.Generic;

namespace Recipes.Contracts
{
    interface IShoppingListShallow
    {
        List<int> Items { get; set; }
    }
}