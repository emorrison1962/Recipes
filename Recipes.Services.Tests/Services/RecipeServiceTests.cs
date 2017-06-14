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
using System.Data;
using Eric.Morrison;

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

        [TestMethod()]
        public void GetAllTest()
        {
            var svc = CreateService();
            var list = svc.GetAll().ToList();

            Assert.IsNotNull(list);
        }

        [TestMethod()]
        public void GetById_Test()
        {
            var svc = CreateService();
            var r = svc.GetById(2);
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


        [TestMethod()]
        public void RecipeService_AuditTest_02()
        {// Add Ingredient Item.
            var svc = CreateService();
            var id = svc.GetAll().Select(x => x.RecipeId).First();
            var server = svc.GetById(id);
            var client = Helpers.Detach(server);

            var group = client.IngredientGroups[0];
            var item = new IngredientItem("XXXX");
            group.Add(item);

            if (!server.Equals(client))
            {
                var changes = server.DetectChanges(client);
                Assert.IsTrue(changes.ModifiedEntities.Count == 1);
                var me = changes.ModifiedEntities.First();
                Assert.IsTrue(me.EntityState == (Recipes.Domain.EntityState)System.Data.Entity.EntityState.Added);
                new object();
            }
        }

        [TestMethod()]
        public void RecipeService_Insert_AdHoc()
        {
            var r = CreateAdHocRecipe();

            r = this.Insert(r);
            this.Delete(r);
        }

        static public Recipe CreateAdHocRecipe()
        {
            var r = new Recipe();
            r.Name = RandomString.GetAlphaOnly(16);
            return r;
        }

        private void Delete(Recipe r)
        {
            var svc = CreateService();
            svc.Delete(r);
        }

        Recipe Insert(Recipe r)
        {
            var svc = CreateService();
            svc.Insert(r);
            return r;
        }

    }//class
}//ns