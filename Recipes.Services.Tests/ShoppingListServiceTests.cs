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
            shoppingList = Detach(shoppingList);

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
            shoppingList = Detach(shoppingList);

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
            shoppingList = Detach(shoppingList);

            shoppingList.Groups.ForEach(g => g.Items.ForEach(i => i.IsChecked = !i.IsChecked));

            shoppingSvc.Update(shoppingList);
            new object();
        }

        public virtual T Detach<T>(T entity)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(entity);
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
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
    }//class
}//ns