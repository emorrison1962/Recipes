using Recipes.Contracts.Services;
using Recipes.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Recipes.Models
{
    [Serializable]
    public class RecipeVM
    {
        IServiceBase<Recipe> _recipeService;
        IServiceBase<Tag> _tagService;

        public IServiceBase<Recipe> RecipeService
        {
            get
            {
                if (null == _recipeService)
                    _recipeService = Unity.Resolve<IServiceBase<Recipe>>();
                return _recipeService;
            }
            set { _recipeService = value; }
        }

        public IServiceBase<Tag> TagService
        {
            get
            {
                if (null == _tagService)
                    _tagService = Unity.Resolve<IServiceBase<Tag>>();
                return _tagService;
            }
            set { _tagService = value; }
        }

        public Recipe Recipe { get; set; }
        public IEnumerable<Tag> TagCatalog { get; set; }
        public ShoppingListVM ShoppingList { get; set; }


        public RecipeVM(int recipeId)
        {
            try
            {
                var r = this.RecipeService.GetById(recipeId);
                var t = this.TagService.GetAll();

                this.Recipe = r;
                Debug.Assert(r.RecipeId > 0);
                this.TagCatalog = t;
                this.ShoppingList = new ShoppingListVM();
            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.ToString());
                throw;
            }
        }

    }//class
}//ns