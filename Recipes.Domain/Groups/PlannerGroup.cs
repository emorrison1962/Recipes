﻿using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipes.Domain
{
    public class PlannerGroup : GroupBase<PlannerGroup, PlannerItem>
    {
        public int PlannerGroupId { get; set; }
        public WeekdayEnum Weekday { get; set; }


        [JsonIgnore]
        [ForeignKey("PlannerId")]
        public Planner Planner { get; set; }
        public int? PlannerId { get; set; }

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
