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
    public class IngredientItemService : ServiceBase<IngredientItem>, IServiceBase<IngredientItem>
    {
        IRepositoryBase<IngredientItem> Repository { get; set; }

        public IngredientItemService(IRepositoryBase<IngredientItem> repository) 
            : base(repository)
        {
            this.Repository = repository;

        }

        override public IngredientItem GetById(int id)
        {
            var result = this.Repository.GetById(id);
            return result;
        }

    }//class
}//ns
