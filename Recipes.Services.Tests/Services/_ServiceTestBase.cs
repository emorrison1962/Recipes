using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Recipes.Services.Tests.Services
{
    public abstract class ServiceTestBase<T> : IServiceTest<T> where T : class
    {
        [TestMethod()]
        abstract public void DeleteById_Test();

        [TestMethod()]
        abstract public void Delete_Test();

        [TestMethod()]
        abstract public void GetAll_Test();

        [TestMethod()]
        abstract public void GetById_Test();

        [TestMethod()]
        abstract public void GetFullObject_Test();

        [TestMethod()]
        abstract public void GetPaged_Test();

        [TestMethod()]
        abstract public void Insert_Test();

        [TestMethod()]
        abstract public void Update_Test();
    }//class
}//ns
