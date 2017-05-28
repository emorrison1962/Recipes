using Eric.Morrison;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recipes.Contracts.Services;
using Recipes.Domain;
using System.Linq;

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

            shoppingList.Groups.ForEach(g => g.Items.ToList().ForEach(i => i.IsChecked = !i.IsChecked));

            var size = GetObjectGraphSize(shoppingList);
            shoppingSvc.Update(shoppingList);
            new object();
        }

        int GetObjectGraphSize(ShoppingList sl)
        {
            var result = 1;

            result += sl.Groups.Count;
            result += sl.Groups.Select(x => x.Items).Count();

            return result;
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
                var changes = server.DetectChanges(client);
                Assert.IsTrue(changes.ModifiedEntities.Count == 1);
                var me = changes.ModifiedEntities.First();
                Assert.IsTrue(me.EntityState == (Recipes.Domain.EntityState)System.Data.Entity.EntityState.Added);
                new object();
            }
        }


    }//class
}//ns