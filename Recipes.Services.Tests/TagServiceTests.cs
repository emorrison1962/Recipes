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
    public class TagServiceTests
    {
        TagService CreateService()
        {
            var ctx = new DataContext();
            IRepositoryBase<Tag> repository = new TagRepository(ctx);
            var result = new TagService(repository);
            return result;
        }

        [TestMethod()]
        public void TagServiceTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetFullObjectTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetPagedTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void InsertTest()
        {
            var list = new List<string>() {
                "Chicken",
                "Beef", 
                "Pork",
                "Seafood",
                "Vegetable",
                "Dessert",
                "Ice Cream"
            };


            var svc = this.CreateService();
            foreach (var s in list)
            {
                var t = new Tag() { Name = s };
                svc.Insert(t);
            }

        }

        [TestMethod()]
        public void UpdateTest()
        {
            Assert.Fail();
        }
    }
}