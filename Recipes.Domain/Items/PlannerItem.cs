using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Runtime.Serialization;
using Recipes.Contracts;

namespace Recipes.Domain
{
    [Serializable]
    public class PlannerItem : GroupItemBase<PlannerItem>
    {

        [UniqueIdentifier]
        public int PlannerItemId { get; set; }

        public int? RecipeId { get; set; }

        [ForeignKey("RecipeId")]
        [NavigationProperty]
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

        [OnDeserializing]
        public void OnDeserializing(StreamingContext ctx)
        {
        }

        [OnDeserialized]
        void OnDeserialized(StreamingContext ctx)
        {
            this.Init();
            if (null != this.PlannerGroup)
            {
                this.PlannerGroupId = this.PlannerGroup.PlannerGroupId;
                this.PlannerGroup = null;
            }
            if (null != this.Recipe)
            {
                this.RecipeId = this.Recipe.RecipeId;
                this.Recipe = null;
            }
        }

        public void Trace()
        {
            throw new NotImplementedException();
        }

    }//class
}//ns
