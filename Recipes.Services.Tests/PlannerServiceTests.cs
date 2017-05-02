using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recipes.Contracts.Repositories;
using Recipes.DAL.Data;
using Recipes.DAL.Repositories;
using Recipes.Domain;
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
    public class PlannerServiceTests
    {
        public static PlannerService CreateService()
        {
            var ctx = new DataContext();
            IRepositoryBase<Planner> repository = new PlannerRepository(ctx);
            IRepositoryBase<PlannerItem> itemRepo = new PlannerItemRepository(ctx);
            var result = new PlannerService(repository, itemRepo);
            return result;
        }


        [TestMethod()]
        public void GetAll_Test()
        {
            var svc = CreateService();
            var result = svc.GetById(-1);
        }

        [TestMethod()]
        public void PlannerService_UpdateTest()
        {
            var svc = CreateService();
            var planner = svc.GetById(-1);
            planner = Helpers.Detach(planner);

            var recipe = this.GetRecipe();
            var item = new PlannerItem(recipe);

            planner.Groups[0].Add(item);

            svc.Update(planner);
        }

        [TestMethod()]
        public void PlannerService_AuditTest_01()
        {
            var svc = CreateService();
            var source = svc.GetById(-1);
            var pending = Helpers.Detach(source);

            // add the recipes.
            const int MAX = 2;
            var recipes = this.GetRecipes(MAX);
            var group = pending.Groups[0];
            foreach (var recipe in recipes)
            {
                var item = Helpers.Detach(new PlannerItem(recipe));
                pending.Groups[0].Add(item);
            }

            if (!source.Equals(pending))
            {
#if false
                var ar = source.AuditChanges(pending);
                Assert.IsTrue(ar.Deltas.Count == 2);
                foreach (var delta in ar.Deltas)
                {
                    Assert.IsTrue(delta.EntityState == EntityState.Added);
                }
                new object();

#endif
            }
            }

        [TestMethod()]
        public void PlannerService_UpdateTest_01()
        {
            try
            {
                var svc = CreateService();
                var source = svc.GetById(-1);
                var pending = Helpers.Detach(source);

                if (false)
                {//Add
                    // add the recipes.
                    const int MAX = 2;
                    var recipes = this.GetRecipes(MAX);
                    var group = pending.Groups[0];
                    foreach (var recipe in recipes)
                    {
                        var item = Helpers.Detach(new PlannerItem(recipe));
                        pending.Groups[0].Add(item);
                    }

                    svc.Update(pending);
                    new object();
                }

                {//Remove
                    var deletions = new List<PlannerItem>();
                    var group = pending.Groups[0];
                    foreach (var item in group.Items)
                    {
                        deletions.Add(item);
                    }
                    deletions.ForEach(x => group.Remove(x));
                    svc.Update(pending);
                    new object();
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            //if (!source.Equals(pending))
            //{
            //    var ar = source.AuditChanges(pending);
            //    Assert.IsTrue(ar.Deltas.Count == 2);
            //    foreach (var delta in ar.Deltas)
            //    {
            //        Assert.IsTrue(delta.EntityState == EntityState.Added);
            //    }
            //    new object();
            //}
        }

        Recipe GetRecipe()
        {
            var recipeSvc = RecipeServiceTests.CreateService();
            var result = recipeSvc.GetAll().LastOrDefault();
            result = Helpers.Detach(result);
            return result;
        }
        List<Recipe> GetRecipes(int count)
        {
            var recipeSvc = RecipeServiceTests.CreateService();
            var result = recipeSvc.GetAll().Take(count).ToList();
            result.ForEach(x => x = Helpers.Detach(x));
            return result;
        }
    }
}