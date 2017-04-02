using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recipes.Contracts.Repositories;
using Recipes.DAL.Data;
using Recipes.DAL.Repositories;
using Recipes.Domain;
using Recipes.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Services.Tests
{
    [TestClass()]
    public class RecipeServiceTests
    {

        public static RecipeService CreateService()
        {
            var ctx = new DataContext();
            IRepositoryBase<Recipe> repository = new RecipeRepository(ctx);
            var result = new RecipeService(repository);
            return result;
        }

        [Ignore]
        [TestMethod()]
        public void RecipeServiceTest()
        {
            Assert.Fail();
        }

        //[Ignore]
        [TestMethod()]
        public void GetAllTest()
        {
            var svc = CreateService();
            var list = svc.GetAll().ToList();

            Assert.IsNotNull(list);
        }

        [TestMethod()]
        public void SeedDatabase()
        {
            #region list
            var list = RecipeUrls.Catalog;
            #endregion

            var svc = CreateService();

            foreach (var url in list)
            {
				try
				{
                    var parser = PageParserFactory.Create(url);
                    var host = new UriBuilder(url).Host;
                    var recipe = parser.TryParse(url);
                    if (recipe.IsValid)
                    {
                        svc.Insert(recipe);
                        new object();
                        Debug.WriteLine(string.Format("ADDED: {0}", url));
                    }
                    else
                    {
                        Debug.WriteLine(string.Format("*** IsValid failed: {0} ***", url));
                    }
                }

#pragma warning disable 168
                catch (Exception ex)
				{
					Debug.WriteLine(url);
					new object();

                    Debug.WriteLine(string.Format("*** Exception: {0} ***", url));

                }
#pragma warning restore 168
            }

        }

        [TestMethod()]
        public void UpdateTest()
        {
            List<Tag> tags = null;
            {
                var e = new TagServiceTests().GetAll();
                tags = e.ToList();
            }

            var svc = CreateService();
            var recipe = svc.GetById(1);
            Assert.IsNotNull(recipe);

            tags.ForEach(t => recipe.Tags.Add(t));

            var result = svc.Update(recipe);
            //Check for accurate update....
        }



    }//class
}//ns