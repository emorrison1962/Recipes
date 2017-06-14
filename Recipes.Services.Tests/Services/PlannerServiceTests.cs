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
using System.Diagnostics;
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
            var pending = source.Detach();

            // add the recipes.
            const int MAX = 2;
            var recipes = this.GetRecipes(MAX);
            var group = pending.Groups[0];
            foreach (var recipe in recipes)
            {
                var item = new PlannerItem(recipe).Detach();
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
            result = result.Detach();
            return result;
        }
        List<Recipe> GetRecipes(int count)
        {
            var recipeSvc = RecipeServiceTests.CreateService();
            var result = recipeSvc.GetAll().Take(count).ToList();
            result.ForEach(x => x = x.Detach());
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
                var pending = source.Detach();

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
                        var item = new PlannerItem(recipe).Detach();
                        pending.Groups[0].Add(item);
                    }

                    pending = pending.Detach();
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

                    pending = pending.Detach();
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
            planner = planner.Detach();

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
                var pending = source.Detach();

                var items = (
                    from g in pending.Groups
                    from i in g.Items
                    select i).ToList();


                var recipe = RecipeServiceTests.CreateAdHocRecipe();
                var recipeName = recipe.Name;
                int itemId = int.MinValue;
                {//Add
                    var group = pending.Groups[0];
                    var item = new PlannerItem(recipe);
                    group.Add(item);

                    pending = svc.Update(pending).Detach();
                    new object();

                    itemId = (
                        from g in pending.Groups
                        from i in g.Items
                        where i.Text == recipeName
                        select (i.PlannerItemId)
                        ).First();
                }
                {//Remove
                    var deletions = new List<PlannerItem>();
                    var item = pending.Groups.Select(x => x.Items.Where(i => i.PlannerItemId == itemId).FirstOrDefault()).Where(x => x != null).First();

                    var group = pending.Groups[0];
                    group.Remove(item);
                    svc = CreateService();
                    pending = svc.Update(pending).Detach();
                    new object();
                }

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


        [TestMethod()]
        public void AddItem_061117()
        {
            try
            {
                var svc = CreateService();
                var source = svc.GetById(-1);
                var pending = source.Detach();

                {//Add
                    const int MAX = 2;
                    for (int i = 0; i < MAX; ++i)
                    {
                        var recipe = RecipeServiceTests.CreateAdHocRecipe();
                        var recipeName = recipe.Name;

                        var group = pending.Groups[0];
                        var item = new PlannerItem(recipe);
                        group.Add(item);
                    }
                    pending = svc.Update(pending).Detach();
                    new object();
                }
            }
#pragma warning disable 0168
            catch (Exception ex)
            {
                throw;
            }
        }

        [TestMethod()]
        public void MoveItem_061117()
        {
            try
            {
                var svc = CreateService();
                var source = svc.GetById(-1);
                var pending = source.Detach();

                {//Move
                    var g0 = pending.Groups[0];
                    var g1 = pending.Groups[1];
                    var item = g0.Items.First();
                    g0.Remove(item);
                    g1.Add(item);

                    pending = svc.Update(pending).Detach();
                    new object();
                }
            }
#pragma warning disable 0168
            catch (Exception ex)
            {
                throw;
            }
        }


        [TestMethod()]
        public void RemovePlannerItem_061117()
        {
            try
            {
                var svc = CreateService();
                var source = svc.GetById(-1);
                var pending = source.Detach();
                {//Remove
                    var deletions = new List<PlannerItem>();
                    var group = pending.Groups.First();
                    var item = group.Items.First();

                    group.Remove(item);
                    svc = CreateService();
                    pending = svc.Update(pending).Detach();
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
        public void DetectChangesTest_01()
        {
            try
            {
                var svc = CreateService();
                var source = svc.GetById(-1).Detach();
                var pending = source.Detach();

                var recipe = RecipeServiceTests.CreateAdHocRecipe();
                var recipeName = recipe.Name;
                int itemId = int.MinValue;
                {//Add
                    var item = new PlannerItem(recipe);
                    pending.Groups[0].Add(item);

                    //var ecr = new EntityChangeResults();
                    //pending.GetNewChildren(ecr);
                    //Debug.WriteLine(ecr);

                    var changes = source.DetectChanges(pending);
                    Debug.WriteLine(changes);
                    new object();

                    itemId = (
                        from g in pending.Groups
                        from i in g.Items
                        where i.Text == recipeName
                        select (i.PlannerItemId)
                        ).First();

                    new object();
                }
                {//Remove
                    source = pending.Detach();

                    var deletions = new List<PlannerItem>();
                    var item = pending.Groups.Select(x => x.Items.Where(i => i.PlannerItemId == itemId).FirstOrDefault()).Where(x => x != null).First();

                    var group = pending.Groups[0];
                    group.Remove(item);


                    pending = pending.Detach();
                    var changes = source.DetectChanges(pending);
                    Debug.WriteLine(changes);
                    new object();
                    //svc.Update(pending);
                    new object();
                }

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