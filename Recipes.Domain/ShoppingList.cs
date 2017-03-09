using System;
using System.Collections.Generic;

namespace Recipes.Domain
{
    [Serializable]
    public class ShoppingList
    {
        public int ShoppingListId { get; set; }
        public HashSet<IngredientGroupItem> Items { get; set; }
        public ShoppingList()
        {
            this.Items = new HashSet<IngredientGroupItem>();
        }

        public void Add(IngredientGroupItem item)
        {
            if (null == item)
                throw new ArgumentNullException("IngredientGroupItem item is null.");

            this.Items.Add(item);
        }
    }//class
}//ns
