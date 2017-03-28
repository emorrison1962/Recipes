using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;

namespace Recipes.Domain
{
    [Serializable]
    public class ShoppingList
    {
        const string DEFAULT_GROUP_TEXT = "<Unknown>";
        public string Text { get; set; }
        public int ShoppingListId { get; set; }
        public List<ShoppingListGroup> Groups { get; set; }

        [NotMapped]
        [JsonIgnore]
        public ShoppingListGroup DefaultGroup
        {
            get
            {
                var result = Groups.FirstOrDefault(x => x.Text == DEFAULT_GROUP_TEXT);
                return result;
            }
        }
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

        [OnSerializing]
        void OnSerializing(StreamingContext ctx)
        {
        }

        [OnSerialized]
        void OnSerialized(StreamingContext ctx)
        {
        }

        [OnDeserializing]
        void OnDeserializing(StreamingContext ctx)
        {
        }

        [OnDeserialized]
        void OnDeserialized(StreamingContext ctx)
        {
        }


    }//class
}//ns
