using Recipes.Dal.Repositories;
using Recipes.DAL.Data;
using Recipes.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.DAL.Repositories
{
    public class ShoppingListGroupRepository : RepositoryBase<ShoppingListGroup>
    {
        public ShoppingListGroupRepository(DataContext dc) : base(dc)
        {
        }

        public override IEnumerable<ShoppingListGroup> GetAll()
        {
            var result = this._dbSet
                .AsQueryable()
                .Include(g => g.Items);

            return result;
        }
    }//class
}//ns
