using Recipes.Dal.Repositories;
using Recipes.DAL.Data;
using Recipes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.DAL.Repositories
{
    public class ShoppingListRepository : RepositoryBase<ShoppingList>
    {
        public ShoppingListRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public override ShoppingList GetFullObject(object id)
        {
            var result = this._dbSet
                .Where(sl => sl.ShoppingListId == (int)id)
                .IncludeMultiple(sl => sl.Items)
                .FirstOrDefault(); 

            return result;
        }

    }//class
}//ns
