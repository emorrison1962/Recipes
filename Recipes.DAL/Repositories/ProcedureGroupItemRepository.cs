using Recipes.Dal.Repositories;
using Recipes.DAL.Data;
using Recipes.Domain;

namespace Recipes.DAL.Repositories
{
    public class ProcedureGroupItemRepository : RepositoryBase<ProcedureGroupItem>
    {
        public ProcedureGroupItemRepository(DataContext dc) : base(dc)
        {
        }
    }

}
