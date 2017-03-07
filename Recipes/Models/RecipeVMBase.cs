using Recipes.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Recipes.Models
{
    public class RecipeVMBase
    {
        public Recipe Recipe { get; set; }
        public IEnumerable<Tag> TagCatalog { get; set; }


        public RecipeVMBase(Recipe r, IEnumerable<Tag> tags)
        {
            this.Recipe = r;
            Debug.Assert(r.RecipeId > 0);
            this.TagCatalog = tags;
        }
    }
}