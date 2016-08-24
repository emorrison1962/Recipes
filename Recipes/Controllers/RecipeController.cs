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

		IRepositoryBase<Recipe> RecipesRepository { get; set; }
		public RecipeController(IRepositoryBase<Recipe> recipesRepository)
		{
			this.RecipesRepository = recipesRepository;

		}
		// GET: Recipe
		public ActionResult Index()
		{
			//var recipes = this.RecipesRepository.GetAll();
			var recipes = new RecipeService().GetAll();


			return View(recipes);
		}



	}
}