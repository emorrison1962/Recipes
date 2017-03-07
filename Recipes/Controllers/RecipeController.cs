using Recipes.Contracts.Repositories;
using Recipes.Domain;
using Recipes.Models;
using Recipes.Services;
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
			var recipe = this.RecipeService.Insert(new Recipe() { Uri = url });

			var result = Json(recipe, JsonRequestBehavior.AllowGet);
			return result;
		}

		[HttpGet]
		public ActionResult View(int recipeId)
		{
			var recipe = this.RecipeService.GetById(recipeId);
			var tags = this.TagService.GetAll();
			var vm = new RecipeVMBase(recipe, tags);

			var result = View(vm);
			return result;
		}

		[HttpGet]
		public ActionResult Update(int recipeId)
		{
			var recipe = this.RecipeService.GetById(recipeId);
			var tags = this.TagService.GetAll();
			var vm = new RecipeVMBase(recipe, tags);

			var result = View(vm);
			return result;
		}

		[HttpGet]
		public ActionResult Delete(int recipeId)
		{
			var recipe = this.RecipeService.GetById(recipeId);
			this.RecipeService.Delete(recipeId);
			return this.RedirectToAction("Index");
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