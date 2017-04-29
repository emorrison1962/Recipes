using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Recipes.Domain
{
    public class PlannerItem : GroupItemBase<PlannerItem>
    {
        [UniqueIdentifier]
        public int PlannerItemId { get; set; }

        public int? RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        virtual public Recipe Recipe { get; set; }

        [JsonIgnore]
        [ForeignKey("PlannerGroupId")]
        public virtual PlannerGroup PlannerGroup { get; set; }
        public int? PlannerGroupId { get; set; }

        public override int PrimaryKey
        {
            get
            {
                return PlannerItemId;
            }
        }

        public PlannerItem()
        {
            this.Init();
        }

        public PlannerItem(Recipe r) : this()
        {
            this.RecipeId = r.RecipeId;
        }
        void Init()
        {
        }


    }
}
