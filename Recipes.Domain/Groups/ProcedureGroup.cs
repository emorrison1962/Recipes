using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Recipes.Domain
{
    [Serializable]
    public class ProcedureGroup : GroupBase<ProcedureGroup, ProcedureItem>
    {
        #region Properties

        public int ProcedureGroupId { get; set; }

        public override int PrimaryKey
        {
            get
            {
                return ProcedureGroupId;
            }
        }

        [JsonIgnore]
        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; }
        public int? RecipeId { get; set; }

        #endregion

        public ProcedureGroup() : base()
        {
            this.Init();
        }
        public ProcedureGroup(string text)
            : base(text)
        {
            this.Init();
        }

        void Init()
        {
        }

        public override void Add(ProcedureItem item)
        {
            this.Items.Add(item);
            item.ProcedureGroup = this;
        }

        public override void Remove(ProcedureItem item)
        {
            this.Items.Remove(item);
            item.ProcedureGroup = null;
        }

        [OnDeserialized]
        public void OnDeserialized(StreamingContext ctx)
        {
            if (null != this.Recipe)
            {
                this.RecipeId = this.Recipe.RecipeId;
                this.Recipe = null;
            }
        }
    }//class
}//ns
