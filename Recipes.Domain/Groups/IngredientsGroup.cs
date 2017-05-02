using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipes.Domain
{
    [Serializable]
    public class IngredientGroup : GroupBase<IngredientGroup, IngredientItem>
    {
        public int IngredientGroupId { get; set; }

        public override int PrimaryKey
        {
            get
            {
                return this.IngredientGroupId;
            }
        }

        public IngredientGroup()
            : base()
        {
            this.Init();
        }

        public IngredientGroup(string text)
            : base(text)
        {
            this.Init();
        }

        void Init()
        {
        }

        public override void Add(IngredientItem item)
        {
            this._items.Add(item);
            item.IngredientGroup = this;
        }

        public override void Remove(IngredientItem item)
        {
            this._items.Remove(item);
            item.IngredientGroup = null;
        }

        protected override void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems.Count > 0)
                foreach (var ob in e.NewItems)
                {
                    var item = ob as IngredientItem;
                    item.IngredientGroup = this;
                }
            if (e.OldItems.Count > 0)
                foreach (var ob in e.OldItems)
                {
                    var item = ob as IngredientItem;
                    item.IngredientGroup = null;
                }
        }
    }//class
}//ns
