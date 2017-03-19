using Recipes.Contracts.Services;
using Recipes.Domain;
using Recipes.Models;
using System.Web.Mvc;

namespace Recipes.Controllers
{
    public class RecipeController : Controller
    {

        IServiceBase<Recipe> RecipeService { get; set; }
        IServiceBase<Tag> TagService { get; set; }
        IShoppingListService ShoppingListService { get; set; }

        public RecipeController(IServiceBase<Recipe> recipeService, IServiceBase<Tag> tagService, IShoppingListService shoppingListService)
        {
            this.RecipeService = recipeService;
            this.TagService = tagService;
            this.ShoppingListService = shoppingListService;

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
            var vm = new RecipeVM(recipeId);

            var result = View(vm);
            return result;
        }

        [HttpGet]
        public ActionResult Update(int recipeId)
        {
            var vm = new RecipeVM(recipeId);

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