using Recipes.Contracts.Services;
using Recipes.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Recipes.Models
{
    public class RecipeVM
    {
        IServiceBase<Recipe> _recipeService;
        IServiceBase<Tag> _tagService;
        IServiceBase<ShoppingList> _shoppingListService;

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

        public IServiceBase<ShoppingList> ShoppingListService
        {
            get
            {
                if (null == _shoppingListService)
                    _shoppingListService = Unity.Resolve<IServiceBase<ShoppingList>>();
                return _shoppingListService;
            }
            set { _shoppingListService = value; }
        }

        public Recipe Recipe { get; set; }
        public IEnumerable<Tag> TagCatalog { get; set; }
        public ShoppingList ShoppingList { get; set; }


        public RecipeVM(int recipeId)
        {
            try
            {
                var r = this.RecipeService.GetById(recipeId);
                var t = this.TagService.GetAll();
                var sl = this.ShoppingListService.GetAll().FirstOrDefault();

                this.Recipe = r;
                Debug.Assert(r.RecipeId > 0);
                this.TagCatalog = t;
                this.ShoppingList = sl;
            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.ToString());
                throw;
            }
        }

    }//class
}//ns