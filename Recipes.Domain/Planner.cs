using System.Collections.Generic;

namespace Recipes.Domain
{
    public class Planner : EntityBase<Planner>
    {
        public int PlannerId { get; set; }
        public List<PlannerGroup> Groups { get; set; }
        public string Text { get; set; }

        public Planner()
        {
            this.Groups = new List<PlannerGroup>();
        }

    }//class
}//ns
