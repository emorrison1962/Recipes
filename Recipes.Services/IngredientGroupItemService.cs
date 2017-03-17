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
    public class IngredientGroupItemService : ServiceBase<IngredientGroupItem>, IServiceBase<IngredientGroupItem>
    {
        IRepositoryBase<IngredientGroupItem> Repository { get; set; }

        public IngredientGroupItemService(IRepositoryBase<IngredientGroupItem> repository) 
            : base(repository)
        {
            this.Repository = repository;

        }

        override public IngredientGroupItem GetById(int id)
        {
            var result = this.Repository.GetById(id);
            return result;
        }

    }//class
}//ns
