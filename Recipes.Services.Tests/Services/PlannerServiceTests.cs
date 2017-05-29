using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recipes.Contracts.Repositories;
using Recipes.Contracts.Services;
using Recipes.DAL.Data;
using Recipes.DAL.Repositories;
using Recipes.Domain;
using Recipes.Services;
using Recipes.Services.Tests.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Services.Tests
{
    [TestClass()]
    public class PlannerServiceTests : ServiceTestBase<PlannerService>
    {
        static IUnityContainer UnityContainer { get; set; }

        [ClassInitialize]
        static public void ClassInitialize(TestContext testCtx)
        {
            UnityContainer = Recipes.Unity.GetConfiguredContainer();
        }



        public static IServiceBase<Planner> CreateService()
        {
            var result = UnityContainer.Resolve<IServiceBase<Planner>>();
            return result;
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

        [TestMethod()]
        public override void GetAll_Test()
        {
            var svc = CreateService();
            var result = svc.GetAll();
            Assert.IsNotNull(result);
        }

        void Insert(Planner p)
        {
            var svc = CreateService();
            svc.Insert(p);
        }

        void Delete(Planner p)
        {
            var svc = CreateService();
            svc.Delete(p);
        }

        [TestMethod()]
        public override void DeleteById_Test()
        {
            var p = RandomEntityExtensions.CreateRandom<Planner>();
            this.Insert(p);
            var svc = CreateService();
            svc.Delete(p.PlannerId);
        }

        [TestMethod()]
        public override void Delete_Test()
        {
            var p = RandomEntityExtensions.CreateRandom<Planner>();
            this.Insert(p);
            this.Delete(p);
        }

        Planner CreateRandom()
        {
            var result = RandomEntityExtensions.CreateRandom<Planner>();
            const int MAX_GROUPS = 6;
            const int MAX_ITEMS = 6;
            for (int g = 0; g < MAX_GROUPS; ++g)
            {
                var group = RandomEntityExtensions.CreateRandom<PlannerGroup>();
                result.Groups.Add(group);
                for (int i = 0; i < MAX_ITEMS; ++i)
                {
                    var item = RandomEntityExtensions.CreateRandom<PlannerItem>();
                    group.Add(item);
                }
            }
            return result;
        }

        [TestMethod()]
        public override void GetById_Test()
        {
            var svc = CreateService();
            var p = svc.GetById(int.MaxValue);
            Assert.IsNotNull(p);
        }

        [TestMethod()]
        public override void GetFullObject_Test()
        {
            var svc = CreateService();
            var p = svc.GetFullObject(int.MaxValue);
            Assert.IsNotNull(p);
        }

        [Ignore]
        [TestMethod()]
        public override void GetPaged_Test()
        {
            //throw new NotImplementedException();
        }

        [TestMethod()]
        public override void Insert_Test()
        {
            var p = RandomEntityExtensions.CreateRandom<Planner>();
            this.Insert(p);
            this.Delete(p);
        }

        [TestMethod()]
        public override void Update_Test()
        {
            try
            {
                var svc = CreateService();
                var source = svc.GetById(-1);
                var pending = Helpers.Detach(source);

                var items = (
                    from g in pending.Groups
                    from i in g.Items
                    select i).ToList();

                const int MAX = 2;
                var recipes = this.GetRecipes(MAX);

                {//Add
                    // add the recipes.
                    var group = pending.Groups[0];
                    foreach (var recipe in recipes)
                    {
                        var item = Helpers.Detach(new PlannerItem(recipe));
                        pending.Groups[0].Add(item);
                    }

                    pending = Helpers.Detach(pending);
                    svc.Update(pending);
                }

                {//Remove
                    var deletions = (
                        from g in pending.Groups
                        from i in g.Items
                        from r in recipes
                        where i.RecipeId == r.RecipeId
                        select i).ToList();

                    var group = pending.Groups[0];
                    foreach (var item in group.Items)
                    {
                        deletions.Add(item);
                    }
                    deletions.ForEach(x => group.Remove(x));

                    pending = Helpers.Detach(pending);
                    svc.Update(pending);
                    new object();
                }

            }
#pragma warning disable 0168
            catch (Exception ex)
            {

                throw;
            }
#pragma warning restore 0168
        }



        [TestMethod()]
        public void UpdateTest_02()
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
        public void UpdateTest_03()
        {
            try
            {
                var svc = CreateService();
                var source = svc.GetById(-1);
                var pending = Helpers.Detach(source);


                var items = (
                    from g in pending.Groups
                    from i in g.Items
                    select i).ToList();


                if (0 == items.Count)
                {//Add
                    // add the recipes.
                    const int MAX = 1;
                    var recipes = this.GetRecipes(MAX);
                    var group = pending.Groups[0];
                    foreach (var recipe in recipes)
                    {
                        var item = Helpers.Detach(new PlannerItem(recipe));
                        pending.Groups[0].Add(item);
                    }

                    pending = Helpers.Detach(pending);
                    svc.Update(pending);
                    new object();
                }
                /*
                {//Remove
                    var deletions = new List<PlannerItem>();
                    var group = pending.Groups[0];
                    foreach (var item in group.Items)
                    {
                        deletions.Add(item);
                    }
                    deletions.ForEach(x => group.Remove(x));


                    pending = Helpers.Detach(pending);
                    svc.Update(pending);
                    new object();
                }
                */
            }
#pragma warning disable 0168
            catch (Exception ex)
            {

                throw;
            }
#pragma warning restore 0168
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

    }//class

}//ns