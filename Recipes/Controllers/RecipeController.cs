using Recipes.Contracts.Repositories;
using Recipes.DAL.Data;
using Recipes.DAL.Repositories;
using Recipes.Domain;
using Recipes.Models;
using Recipes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recipes.Controllers
{
	public class RecipeController : Controller
	{

		RecipeService RecipeService { get; set; }
		TagService TagService { get; set; }

		public RecipeController(IRepositoryBase<Recipe> recipeRepository, IRepositoryBase<Tag> tagRepository)
		{
			this.RecipeService = new RecipeService(recipeRepository);
			this.TagService = new TagService(tagRepository);
		}
		// GET: Recipe
		public ActionResult Index()
		{
			var recipes = this.RecipeService.GetAll();

			return View(recipes);
		}

		public JsonResult Insert(string url)
		{
			var recipe = this.RecipeService.Insert(new Recipe() { Uri = url});

			var result = Json(recipe, JsonRequestBehavior.AllowGet);
			return result;
		}

        [HttpGet]
        public ActionResult Update(int recipeId)
		{
            var recipe = this.RecipeService.GetById(recipeId);
			var tags = this.TagService.GetAll();
			var vm = new EditRecipeVM(recipe, tags);

			var result = View(vm);
			return result;
		}

        [HttpPost]
        public ActionResult UpdateRecipe(Recipe recipe)
		{
            this.RecipeService.Update(recipe);

            //var result = Json(recipe, JsonRequestBehavior.AllowGet);
            //return result;
            return this.RedirectToAction("Index");
        }

    }
}