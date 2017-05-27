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

        public override void Add(ShoppingListItem item)
        {
            this._items.Add(item);
            item.ShoppingListGroup = this;
        }

        public override void Remove(ShoppingListItem item)
        {
            this._items.Remove(item);
            item.ShoppingListGroup = null;
        }

        protected override void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems.Count > 0)
                foreach (var ob in e.NewItems)
                {
                    var item = ob as ShoppingListItem;
                    item.ShoppingListGroup = this;
                }
            if (e.OldItems.Count > 0)
                foreach (var ob in e.OldItems)
                {
                    var item = ob as ShoppingListItem;
                    item.ShoppingListGroup = null;
                }
        }
    }
}
