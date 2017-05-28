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
            this.Items.Add(item);
            item.PlannerGroup = this;
        }

        public override void Remove(PlannerItem item)
        {
            this.Items.Remove(item);
            item.PlannerGroup = null;
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
            if (null != this.Planner)
            {
                this.PlannerId = this.Planner.PlannerId;
                this.Planner = null;
            }
            if (null != this.Items)
            {
                foreach (var item in this.Items)
                {
                    item.PlannerGroupId = this.PlannerGroupId;
                }
            }
        }

    }//class
}//ns
