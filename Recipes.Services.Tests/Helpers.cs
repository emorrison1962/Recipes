﻿namespace Recipes.Services.Tests
{
    public class Helpers
    {
        static public T Detach<T>(T entity)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(entity);
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            return result;
        }

    }
}
