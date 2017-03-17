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
                this.ShoppingList = new ShoppingListVM();
                this.ShoppingList.Load();
                this.TagCatalog = t;

                //Set the IsChecked flag on appropriate recipe ingredients.
                var seq = (
                    from ig in this.Recipe.IngredientGroups
                    from igi in ig.Items
                    from sli in this.ShoppingList.Items
                    where igi.IngredientGroupItemId == sli.IngredientGroupItemId
                    select (igi)).ToList();
                seq.ForEach(x => x.IsChecked = true);

            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.ToString());
                throw;
            }
        }

    }//class
}//ns