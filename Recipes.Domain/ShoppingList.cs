using System;
using System.Collections.Generic;
using System.Linq;

namespace Recipes.Domain
{
    [Serializable]
    public class ShoppingList
    {
        public int ShoppingListId { get; set; }
        public List<ShoppingListGroup> Groups { get; set; }
        public ShoppingList()
        {
            this.Groups = new List<ShoppingListGroup>();
        }

        public void Add(ShoppingListItem item)
        {
            if (null == item)
                throw new ArgumentNullException("IngredientItem item is null.");

            this.Groups.First().Add(item);
        }
    }//class
}//ns
