using Newtonsoft.Json;
using Recipes.Contracts.Services;
using Recipes.Domain;
using Recipes.Models;
using System.IO;
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
            var planner = this.PlannerService.GetById(-1);
            var recipes = this.RecipeService.GetAll();
            var vm = new PlannerVM(planner, recipes);
            return View(vm);
        }

        [HttpPost]
        public ActionResult Update()
        {
            var planner = this.DeserializeJson<Planner>();
            this.PlannerService.Update(planner);
            return null;
        }

        T DeserializeJson<T>() where T : EntityBase
        {
            var json = string.Empty;
            this.HttpContext.Request.InputStream.Position = 0;
            using (StreamReader inputStream = new StreamReader(this.HttpContext.Request.InputStream))
            {
                json = inputStream.ReadToEnd();
            }
            var result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }

    }//class
}//ns