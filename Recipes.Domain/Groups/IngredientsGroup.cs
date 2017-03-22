using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipes.Domain
{
    [Serializable]
    public class IngredientGroup : GroupBase<IngredientItem>
    {
        public int IngredientGroupId { get; set; }

        public IngredientGroup()
            : base()
        {  }

        public IngredientGroup(string text)
            : base(text)
        {  }

    }//class
}//ns
