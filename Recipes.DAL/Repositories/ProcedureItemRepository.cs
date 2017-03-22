using Recipes.Dal.Repositories;
using Recipes.DAL.Data;
using Recipes.Domain;

namespace Recipes.DAL.Repositories
{
    public class ProcedureItemRepository : RepositoryBase<ProcedureItem>
    {
        public ProcedureItemRepository(DataContext dc) : base(dc)
        {
        }
    }

}
