using Recipes.Contracts.Services;
using Recipes.Domain;
using Recipes.Models;
using System.Linq;
using System.Web.Mvc;

namespace Recipes.Controllers
{
    public class ShoppingListController : Controller
    {
        IServiceBase<ShoppingList> ShoppingListService { get; set; }

        public ShoppingListController(IServiceBase<ShoppingList> shoppingListService)
        {
            this.ShoppingListService = shoppingListService;
        }
        // GET: ShoppingList
        public ActionResult Index()
        {
            var shoppingList = this.ShoppingListService.GetFullObject(int.MinValue);
            return View(shoppingList);
        }

        [HttpPost]
        public ActionResult UpdateShoppingList(ShoppingList shoppingList)
        {
            this.ShoppingListService.Update(shoppingList);

            return null;
        }


    }//class
}//ns