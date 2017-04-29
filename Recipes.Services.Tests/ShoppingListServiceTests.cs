using Eric.Morrison;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recipes.Contracts.Services;
using Recipes.Domain;
using Recipes.Models;
using Recipes.Services;
using System;
using System.Collections.Generic;
using System.Data;
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
            UnityContainer = Recipes.Unity.GetConfiguredContainer();
            //var unity =  new UnityConfig();
            //UnityContainer = UnityConfig.GetConfiguredContainer();
            //var shoppingSvc = UnityContainer.Resolve<IServiceBase<ShoppingList>>();
        }

        [TestMethod()]
        public void Update_AddIngredient_Test()
        {
            var shoppingSvc = UnityContainer.Resolve<IServiceBase<ShoppingList>>();
            var shoppingList = shoppingSvc.GetFullObject(int.MinValue);
            shoppingList = Helpers.Detach(shoppingList);

            var ingredientItem = this.GetRandomIngredient();
            var shoppingItem = new ShoppingListItem();
            shoppingItem.Text = ingredientItem.Text;
            shoppingList.DefaultGroup.Add(shoppingItem);

            shoppingSvc.Update(shoppingList);
            new object();
        }


        [TestMethod()]
        public void Update_ManuallyEnteredIngredient_Test()
        {
            var shoppingSvc = UnityContainer.Resolve<IServiceBase<ShoppingList>>();
            var shoppingList = shoppingSvc.GetFullObject(int.MinValue);
            shoppingList = Helpers.Detach(shoppingList);

            var shoppingItem = new ShoppingListItem();
            shoppingItem.Text = RandomString.GetAlphaOnly(RandomValue.Next<uint>(12, 24));
            shoppingList.DefaultGroup.Add(shoppingItem);

            shoppingSvc.Update(shoppingList);
            new object();
        }

        [TestMethod()]
        public void Update_UpdateIngredient_Test()
        {
            var shoppingSvc = UnityContainer.Resolve<IServiceBase<ShoppingList>>();
            var shoppingList = shoppingSvc.GetFullObject(int.MinValue);
            shoppingList = Helpers.Detach(shoppingList);

            shoppingList.Groups.ForEach(g => g.Items.ForEach(i => i.IsChecked = !i.IsChecked));

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

        [TestMethod()]
        public void Planner_AuditTest_01()
        {// Change Name.
            var shoppingSvc = UnityContainer.Resolve<IServiceBase<ShoppingList>>();
            var server = shoppingSvc.GetFullObject(int.MinValue);
            var client = Helpers.Detach(server);

            var shoppingItem = new ShoppingListItem();
            shoppingItem.Text = RandomString.GetAlphaOnly(RandomValue.Next<uint>(12, 24));
            client.DefaultGroup.Add(shoppingItem);

            if (!server.Equals(client))
            {
                var ar = server.AuditChanges(client);
                Assert.IsTrue(ar.Deltas.Count == 1);
                var delta = ar.Deltas.First();
                Assert.IsTrue(delta.EntityState == EntityState.Added);
                new object();
            }
        }


    }//class
}//ns