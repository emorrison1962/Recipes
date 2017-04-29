using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;

namespace Recipes.Domain
{
    [Serializable]
    public class ShoppingListGroup : GroupBase<ShoppingListGroup, ShoppingListItem>
    {
        public int ShoppingListGroupId { get; set; }

        //Navigation property
        [JsonIgnore]
        public virtual ShoppingList ShoppingList { get; set; }

        public override int PrimaryKey
        {
            get
            {
                return ShoppingListGroupId;
            }
        }

        public ShoppingListGroup() : base()
        {
            this.Init();
        }

        void Init()
        {
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
            //this.Init();
        }


    }
}
