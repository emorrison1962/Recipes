using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Recipes.Domain
{
    [Serializable]
    public class IngredientGroup : GroupBase<IngredientGroup, IngredientItem>
    {
        public int IngredientGroupId { get; set; }

        public override int PrimaryKey
        {
            get
            {
                return this.IngredientGroupId;
            }
        }

        [JsonIgnore]
        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; }
        public int? RecipeId { get; set; }



        public IngredientGroup()
            : base()
        {
            this.Init();
        }

        public IngredientGroup(string text)
            : base(text)
        {
            this.Init();
        }

        void Init()
        {
        }

        public override void Add(IngredientItem item)
        {
            this.Items.Add(item);
            item.IngredientGroup = this;
        }

        public override void Remove(IngredientItem item)
        {
            this.Items.Remove(item);
            item.IngredientGroup = null;
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
