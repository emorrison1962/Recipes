using Microsoft.Practices.Unity;
using Recipes.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recipes
{
    static class Unity 
    {
        static public T Resolve<T>()
        {
            var container = UnityConfig.GetConfiguredContainer();
            var result = container.Resolve<T>();
            return result;
        }
    }//class
}//ns