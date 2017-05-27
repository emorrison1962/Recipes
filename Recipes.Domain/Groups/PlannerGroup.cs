using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using Recipes.Contracts;

namespace Recipes.Domain
{
    [Serializable]
    public class PlannerGroup : GroupBase<PlannerGroup, PlannerItem>
    {
        [UniqueIdentifier]
        public int PlannerGroupId { get; set; }
        public WeekdayEnum Weekday { get; set; }


        [JsonIgnore]
        [ForeignKey("PlannerId")]
        public Planner Planner { get; set; }
        public int? PlannerId { get; set; }

        override public string Text { get { return Weekday.ToString(); } set { } }

        public override int PrimaryKey
        {
            get
            {
                return this.PlannerGroupId;
            }
        }

        public PlannerGroup()
        {
        }

        public PlannerGroup(string text) : base(text)
        {
            throw new NotSupportedException();
        }

        public PlannerGroup(WeekdayEnum weekday) : this() 
        {
            this.Text = weekday.ToString();
            this.Weekday = weekday;
        }

        public override void Add(PlannerItem item)
        {
            this._items.Add(item);
            item.PlannerGroup = this;
        }

        public override void Remove(PlannerItem item)
        {
            this._items.Remove(item);
            item.PlannerGroup = null;
        }

        protected override void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems.Count > 0)
                foreach (var ob in e.NewItems)
                {
                    var item = ob as PlannerItem;
                    item.PlannerGroup = this;
                }
            if (e.OldItems.Count > 0)
                foreach (var ob in e.OldItems)
                {
                    var item = ob as PlannerItem;
                    item.PlannerGroup = null;
                }
        }


        public void Trace()
        {
            throw new NotImplementedException();
        }

        void Init() { }

        [OnDeserializing]
        public void OnDeserializing(StreamingContext ctx)
        {
        }


        [OnDeserialized]
        void OnDeserialized(StreamingContext ctx)
        {
            this.Init();
            if (null != this.Items)
            {
                foreach (var item in this.Items)
                {
                    item.PlannerGroupId = this.PlannerGroupId;
                    //item.PlannerGroup = this;
                }
            }
        }

    }//class
}//ns
