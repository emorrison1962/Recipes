using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Recipes.Domain
{
    [Serializable]
    public class ProcedureGroup : GroupBase<ProcedureGroup, ProcedureItem>
    {
        public int ProcedureGroupId { get; set; }

        public override int PrimaryKey
        {
            get
            {
                return ProcedureGroupId;
            }
        }

        public ProcedureGroup() : base()
        {
            this.Init();
        }
        public ProcedureGroup(string text)
            : base(text)
        {
            this.Init();
        }

        void Init()
        {
        }

        public override void Add(ProcedureItem item)
        {
            this._items.Add(item);
            item.ProcedureGroup = this;
        }

        public override void Remove(ProcedureItem item)
        {
            this._items.Remove(item);
            item.ProcedureGroup = null;
        }

        protected override void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems.Count > 0)
                foreach (var ob in e.NewItems)
                {
                    var item = ob as ProcedureItem;
                    item.ProcedureGroup = this;
                }
            if (e.OldItems.Count > 0)
                foreach (var ob in e.OldItems)
                {
                    var item = ob as ProcedureItem;
                    item.ProcedureGroup = null;
                }
        }
    }//class
}//ns
