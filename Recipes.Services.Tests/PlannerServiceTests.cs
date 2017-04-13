using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recipes.Contracts.Repositories;
using Recipes.DAL.Data;
using Recipes.DAL.Repositories;
using Recipes.Domain;
using Recipes.Services;
using System;
using System.Collections.Generic;
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

            planner.Groups[0].Items.Add(item);

            svc.Update(planner);
        }

        [TestMethod()]
        public void PlannerService_UpdateTest02()
        {
            var svc = CreateService();
            var planner = svc.GetById(-1);
            planner = Helpers.Detach(planner);

            const int MAX = 10;
            var recipes = this.GetRecipes(MAX);
            foreach (var recipe in recipes)
            {
                var item = new PlannerItem(recipe);
                planner.Groups[0].Items.Add(item);
            }

            svc.Update(planner);
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