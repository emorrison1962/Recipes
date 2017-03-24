using Eric.Morrison;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recipes.Contracts.Services;
using Recipes.Domain;
using Recipes.Models;
using Recipes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Services.Tests
{
    [TestClass()]
    public class ShoppingListServiceTests
    {

        static IUnityContainer UnityContainer { get; set; }

        [ClassInitialize]
        static public void ClassInitialize(TestContext testCtx)
        {
            var unity =  new UnityConfig();
            UnityContainer = UnityConfig.GetConfiguredContainer();
        }

        [TestMethod()]
        public void Update_AddIngredient_Test()
        {
            var shoppingSvc = UnityContainer.Resolve<IServiceBase<ShoppingList>>();
            var shoppingList = shoppingSvc.GetFullObject(int.MinValue);

            var ingredientItem = this.GetRandomIngredient();
            var shoppingItem = new ShoppingListItem();
            shoppingItem.Text = ingredientItem.Text;    

            shoppingSvc.Update(shoppingList);
            new object();
        }

        IngredientItem GetRandomIngredient()
        {
            var ingredientItemSvc = UnityContainer.Resolve<IngredientItemService>();
            var ingredients = ingredientItemSvc.GetAll().ToList();
            var randomList = new RandomList<IngredientItem>(ingredients);
            var result = randomList.Next();
            return result;
        }
    }//class
}//ns