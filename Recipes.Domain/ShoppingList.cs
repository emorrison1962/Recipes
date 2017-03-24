using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Recipes.Domain
{
    [Serializable]
    public class ShoppingList
    {
        public string Text { get; set; }
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

        [OnDeserialized]
        public void OnDeserialized(StreamingContext ctx)
        {
            new Object();
        }

        [OnDeserializing]
        public void OnDeserializing(StreamingContext ctx)
        {
            new Object();
        }

    }//class
}//ns
