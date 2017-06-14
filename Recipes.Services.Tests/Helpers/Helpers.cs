namespace Recipes.Services.Tests
{
    public static class Helpers
    {
        static public T Detach<T>(this T entity)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(entity);
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            return result;
        }

    }
}
