using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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

    }//class
}//ns
