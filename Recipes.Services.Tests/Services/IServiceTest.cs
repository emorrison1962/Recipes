namespace Recipes.Services.Tests.Services
{
    interface IServiceTest<T>
    {
        void Delete_Test();
        void DeleteById_Test();
        void GetAll_Test();
        void GetById_Test();
        void GetFullObject_Test();
        void GetPaged_Test();
        void Insert_Test();
        void Update_Test();
    }
}
