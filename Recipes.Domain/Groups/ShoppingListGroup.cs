using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Collections.Specialized;

namespace Recipes.Domain
{
    [Serializable]
    public class ShoppingListGroup : GroupBase<ShoppingListGroup, ShoppingListItem>
    {
        public int ShoppingListGroupId { get; set; }

        //Navigation property
        [JsonIgnore]
        [ForeignKey("ShoppingListId")]
        public virtual ShoppingList ShoppingList { get; set; }
        public int? ShoppingListId { get; set; }



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

        public override void Add(ShoppingListItem item)
        {
            this.Items.Add(item);
            item.ShoppingListGroup = this;
        }

        public override void Remove(ShoppingListItem item)
        {
            this.Items.Remove(item);
            item.ShoppingListGroup = null;
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
            this.Init();
            if (null != this.ShoppingList)
            {
                this.ShoppingListId = this.ShoppingList.ShoppingListId;
                this.ShoppingList = null;
            }
            if (null != this.Items)
            {
                foreach (var item in this.Items)
                {
                    item.ShoppingListGroupId = this.ShoppingListGroupId;
                }
            }
        }
    }//class
}//ns
