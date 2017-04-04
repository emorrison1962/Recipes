using Newtonsoft.Json;

namespace Recipes.Domain
{
    public class PlannerItem : GroupItemBase
    {
        public int PlannerItemId { get; set; }
        public int RecipeId { get; set; }
        virtual public Recipe Recipe { get; set; }

        [JsonIgnore]
        public virtual PlannerGroup PlannerGroup { get; set; }


        public PlannerItem()
        {
        }

        public PlannerItem(Recipe r)
        {
            this.Recipe = r;
        }

    }
}
