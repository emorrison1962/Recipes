using Recipes.Contracts.Services;
using Recipes.Domain;
using System.Web.Mvc;

namespace Recipes.Controllers
{
    public class ShoppingListController : Controller
    {
        IServiceBase<ShoppingList> ShoppingListService { get; set; }
        IServiceBase<ShoppingListGroup> ShoppingListGroupService { get; set; }

        public ShoppingListController(IServiceBase<ShoppingList> shoppingListService, IServiceBase<ShoppingListGroup> shoppingListGroupService)
        {
            this.ShoppingListService = shoppingListService;
            this.ShoppingListGroupService = shoppingListGroupService;
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

        public ActionResult EditItems()
        {
            var shoppingList = this.ShoppingListService.GetFullObject(int.MinValue);
            return View(shoppingList);
        }

    }//class
}//ns