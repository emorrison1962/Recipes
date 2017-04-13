using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Domain
{
    public class PlannerGroup : GroupBase<PlannerGroup, PlannerItem>
    {
        public int PlannerGroupId { get; set; }
        public WeekdayEnum Weekday { get; set; }
        [JsonIgnore]
        public Planner Planner { get; set; }

        override public string Text { get { return Weekday.ToString(); } set { } }


        public PlannerGroup()
        {
        }

        public PlannerGroup(string text) : base(text)
        {
            throw new NotSupportedException();
        }

        public PlannerGroup(WeekdayEnum weekday) : base(weekday.ToString())
        {
            this.Weekday = weekday;
        }
        
    }
}
