using Microsoft.Practices.Unity;
using Recipes.Contracts.Repositories;
using Recipes.Contracts.Services;
using Recipes.DAL.Repositories;
using Recipes.Domain;
using Recipes.Services;
using System;

namespace Recipes.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IRepositoryBase<Recipe>, RecipeRepository>();
            container.RegisterType<IRepositoryBase<Tag>, TagRepository>();
            container.RegisterType<IRepositoryBase<ShoppingList>, ShoppingListRepository>();
            container.RegisterType<IRepositoryBase<IngredientGroup>, IngredientGroupRepository>();
            container.RegisterType<IRepositoryBase<IngredientItem>, IngredientItemRepository>();
            container.RegisterType<IRepositoryBase<ShoppingListGroup>, ShoppingListGroupRepository>();


            container.RegisterType<IServiceBase<Recipe>, RecipeService>();
            container.RegisterType<IServiceBase<Tag>, TagService>();
            container.RegisterType<IServiceBase<ShoppingListGroup>, ShoppingListGroupService>();
            container.RegisterType<IServiceBase<ShoppingList>, ShoppingListService>();
            container.RegisterType<IServiceBase<IngredientGroup>, IngredientGroupService>();
            container.RegisterType<IServiceBase<IngredientItem>, IngredientItemService>();

        }
    }//class

}//ns
