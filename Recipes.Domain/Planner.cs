using System;
using System.Collections.Generic;

namespace Recipes.Domain
{
    [Serializable]
    public class Planner : EntityBase<Planner>
    {
        [UniqueIdentifier]
        public int PlannerId { get; set; }
        public List<PlannerGroup> Groups { get; set; }
        public string Text { get; set; }

        public override int PrimaryKey
        {
            get
            {
                return PlannerId;
            }
        }

        public Planner()
        {
            this.Groups = new List<PlannerGroup>();
        }

    }//class
}//ns
