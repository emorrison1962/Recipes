using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace Recipes.Services.Tests.DbSeeds
{
    [TestClass]
    public class DbSeed
    {
        [Ignore]
        [TestMethod()]
        public void RecipesSeed()
        {
            #region list
            var list = RecipeUrls.Catalog;
            #endregion

            var svc = RecipeServiceTests.CreateService();

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

    }
}
