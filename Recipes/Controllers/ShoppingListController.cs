using Recipes.Contracts.Services;
using Recipes.Domain;
using Recipes.Models;
using System.Linq;
using System.Web.Mvc;

namespace Recipes.Controllers
{
    public class ShoppingListController : Controller
    {
        IShoppingListService ShoppingListService { get; set; }

        public ShoppingListController(IShoppingListService shoppingListService)
        {
            this.ShoppingListService = shoppingListService;
        }
        // GET: ShoppingList
        public ActionResult Index()
        {
            var shoppingList = this.ShoppingListService.GetAll().LastOrDefault();
            return View(shoppingList);
        }

        [HttpPost]
        public ActionResult UpdateShoppingList(ShoppingList sl)
        {
            this.ShoppingListService.Update(sl);

            return null;
        }


    }//class
}//ns