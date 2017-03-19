using Recipes.Contracts.Services;
using Recipes.Models;
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
            var shoppingList = new ShoppingListVM();
            shoppingList.Load(false);

            return View(shoppingList);
        }

        [HttpPost]
        public ActionResult UpdateShoppingList(ShoppingListVM vm)
        {
            this.ShoppingListService.Update(vm.ShoppingListId, vm.Items);

            return null;
        }


    }//class
}//ns