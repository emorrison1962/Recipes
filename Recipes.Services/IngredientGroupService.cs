﻿using Recipes.Contracts.Repositories;
using Recipes.Contracts.Services;
using Recipes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Services
{
    public class IngredientGroupService : ServiceBase<IngredientGroup>, IServiceBase<IngredientGroup>
    {
        public IngredientGroupService(IRepositoryBase<IngredientGroup> repository) 
            : base(repository)
        {
        }

    }//class
}//ns
