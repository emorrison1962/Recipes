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
    }
}