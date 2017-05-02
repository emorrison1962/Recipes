using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Runtime.Serialization;

namespace Recipes.Domain
{
    [Serializable]
    public class PlannerItem : GroupItemBase<PlannerItem>
    {
        [JsonIgnore]
        public PlannerGroup _group { get; set; }

        [UniqueIdentifier]
        public int PlannerItemId { get; set; }

        public int? RecipeId { get; set; }

        [ForeignKey("RecipeId")]
        virtual public Recipe Recipe { get; set; }


        [JsonIgnore]
        [ForeignKey("PlannerGroupId")]
        public virtual PlannerGroup PlannerGroup
        {
            get { return _group; }
            set
            {
                _group = value;
                _group.Add(this);
                this.PlannerGroupId = _group.PlannerGroupId;
            }
        }
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

        [OnDeserialized]
        void OnDeserialized(StreamingContext ctx)
        {
            this.Init();
            if (null != this.PlannerGroup)
                this.PlannerGroupId = this.PlannerGroup.PlannerGroupId;
        }
    }
}
