using Microsoft.Practices.Unity;
using Recipes.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recipes
{
    public static class Unity 
    {
        static public IUnityContainer GetConfiguredContainer()
        {
            var result = UnityConfig.GetConfiguredContainer();
            return result;
        }
        static public T Resolve<T>()
        {
            var container = UnityConfig.GetConfiguredContainer();
            var result = container.Resolve<T>();
            return result;
        }
    }//class
}//ns