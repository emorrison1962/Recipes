using Recipes.Contracts.Repositories;
using Recipes.Contracts.Services;
using Recipes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Services
{
    public class IngredientGroupItemService : IServiceBase<IngredientGroupItem>
    {
        IRepositoryBase<IngredientGroupItem> Repository { get; set; }

        public IngredientGroupItemService(IRepositoryBase<IngredientGroupItem> repository)
        {
            this.Repository = repository;

        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(IngredientGroupItem entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IngredientGroupItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IngredientGroupItem> GetAll(object filter)
        {
            throw new NotImplementedException();
        }

        public IngredientGroupItem GetById(int id)
        {
            var result = this.Repository.GetById(id);
            return result;
        }

        public IngredientGroupItem GetFullObject(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IngredientGroupItem> GetPaged(int top = 20, int skip = 0, object orderBy = null, object filter = null)
        {
            throw new NotImplementedException();
        }

        public IngredientGroupItem Insert(IngredientGroupItem entity)
        {
            throw new NotImplementedException();
        }

        public IngredientGroupItem Update(IngredientGroupItem entity)
        {
            throw new NotImplementedException();
        }
    }
}
