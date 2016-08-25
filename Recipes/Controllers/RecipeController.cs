using Recipes.Contracts.Repositories;
using Recipes.DAL.Data;
using Recipes.DAL.Repositories;
using Recipes.Domain;
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

        public RecipeController(IRepositoryBase<Recipe> recipesRepository)
		{
			this.RecipeService = new RecipeService(recipesRepository);

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



	}
}