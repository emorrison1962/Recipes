using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipes.Domain
{
    public class PlannerItem : GroupItemBase<PlannerItem>
    {
        public int PlannerItemId { get; set; }

        public int RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        virtual public Recipe Recipe { get; set; }

        public int PlannerGroupId { get; set; }
        [JsonIgnore]
        [ForeignKey("PlannerGroupId")]
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
