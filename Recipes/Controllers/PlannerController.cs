using Recipes.Contracts.Services;
using Recipes.Domain;
using Recipes.Models;
using System.Linq;
using System.Web.Mvc;

namespace Recipes.Controllers
{
    public class PlannerController : Controller
    {
        IServiceBase<Recipe> RecipeService { get; set; }
        IServiceBase<Planner> PlannerService { get; set; }

        public PlannerController(IServiceBase<Planner> plannerService, IServiceBase<Recipe> recipeService)
        {
            this.PlannerService = plannerService;
            this.RecipeService = recipeService;
        }
        // GET: Planner
        public ActionResult Index()
        {
            var planner = this.PlannerService.GetAll().First();
            var recipes = this.RecipeService.GetAll();
            var vm = new PlannerVM(planner, recipes);
            return View(vm);
        }
    }
}