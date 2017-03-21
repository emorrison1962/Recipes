using Eric.Morrison;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void UpdateTest()
        {
            var svc = UnityContainer.Resolve<ShoppingListService>();

            var sl1 = new ShoppingListVM();
            sl1.Load(false);

            
            var igi = new IngredientGroupItem(RandomString.GetAlphaOnly(RandomValue.Next<uint>(12, 24)));
            sl1.Items.Add(igi);

            svc.Update(sl1.ShoppingListId, sl1.Items);

            var sl2 = new ShoppingListVM();
            sl2.Load(false);

            Assert.AreEqual(sl1.Items.Count, sl2.Items.Count);

        }
    }
}