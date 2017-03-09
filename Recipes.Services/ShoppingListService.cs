using Recipes.Contracts.Repositories;
using Recipes.Contracts.Services;
using Recipes.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Recipes.Services
{
    public class ShoppingListService : IServiceBase<ShoppingList>
    {
        IRepositoryBase<ShoppingList> Repository { get; set; }

        public ShoppingListService(IRepositoryBase<ShoppingList> repository)
        {
            this.Repository = repository;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(ShoppingList entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ShoppingList> GetAll()
        {
            List<ShoppingList> result;
            try
            {
#warning We're only supporting one global list right now.
                result = this.Repository.GetAll().ToList();
                result.Sort();
            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.ToString());
                throw;
            }
            return result;
        }

        public IEnumerable<ShoppingList> GetAll(object filter)
        {
            throw new NotImplementedException();
        }

        public ShoppingList GetById(int id)
        {
            throw new NotImplementedException();
        }

        public ShoppingList GetFullObject(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ShoppingList> GetPaged(int top = 20, int skip = 0, object orderBy = null, object filter = null)
        {
            throw new NotImplementedException();
        }

        public ShoppingList Insert(ShoppingList entity)
        {
            try
            {
                this.Repository.Insert(entity);
                this.Repository.Commit();
            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.ToString());
                throw;
            }

            return entity;
        }

        public ShoppingList Update(ShoppingList entity)
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

            return this.Repository.GetById(entity.ShoppingListId);
        }
    }
}
