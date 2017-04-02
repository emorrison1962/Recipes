using Recipes.Contracts.Repositories;
using Recipes.Contracts.Services;
using Recipes.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Recipes.Services
{
    public class ShoppingListService : ServiceBase<ShoppingList>, IServiceBase<ShoppingList>
    {
        #region Fields


        #endregion

        #region Properties

        IServiceBase<ShoppingListGroup> ShoppingListGroupService { get; set; }

        #endregion

        #region Construction

        public ShoppingListService(IRepositoryBase<ShoppingList> repository,
            IServiceBase<ShoppingListGroup> shoppingListGroupService)
            : base(repository)
        {
            this.ShoppingListGroupService = shoppingListGroupService;
        }

        #endregion

        override public IEnumerable<ShoppingList> GetAll()
        {
            List<ShoppingList> result;
            try
            {
                result = this.Repository.GetAll().ToList();
            }
#pragma warning disable 0168
            catch (Exception ex)
            {
                throw;
            }
#pragma warning restore 0168
            return result;
        }

        override public ShoppingList Insert(ShoppingList entity)
        {
            try
            {
                this.Repository.Insert(entity);
                this.Repository.Commit();
            }
#pragma warning disable 0168
            catch (Exception ex)
            {
                throw;
            }
#pragma warning restore 0168

            return entity;
        }

        override public ShoppingList Update(ShoppingList entity)
        {
            try
            {
                this.Repository.Update(entity);
                this.Repository.Commit();
            }
#pragma warning disable 168
            catch (Exception ex)
            {
                throw;
            }
#pragma warning restore 168

            return entity;
        }

        class ShoppingListItemIdComparer : IEqualityComparer<ShoppingListItem>
        {
            public bool Equals(ShoppingListItem x, ShoppingListItem y)
            {
                return x.ShoppingListItemId == y.ShoppingListItemId;
            }

            public int GetHashCode(ShoppingListItem obj)
            {
                return obj.ShoppingListItemId.GetHashCode();
            }
        }

    }//class
}//ns
