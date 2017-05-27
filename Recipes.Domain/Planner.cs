using Recipes.Contracts;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

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

        [OnDeserializing]
        public void OnDeserializing(StreamingContext ctx)
        {
        }


        [OnDeserialized]
        public void OnDeserialized(StreamingContext ctx)
        {
            if (null != this.Groups)
            {
                foreach (var group in this.Groups)
                {
                    //group.Planner = this;
                    group.PlannerId = this.PlannerId;
                }
            }
        }

#if false
        public void Trace()
        {
            const string FORMAT_START = @"
{0}
{{
    PlannerId={1}
    Text={2}
    Groups=
    {{
";
            const string FORMAT_END = @"
    }}
}}";

            System.Diagnostics.Debug.WriteLine(string.Format(FORMAT_START, this.ToString(), this.PlannerId, this.Text));
            Groups.ForEach(g => g.Trace());
            System.Diagnostics.Debug.WriteLine(FORMAT_END);
        }

#endif
    }//class
}//ns
