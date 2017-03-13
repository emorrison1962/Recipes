using Recipes.Dal.Repositories;
using Recipes.DAL.Data;
using Recipes.Domain;

namespace Recipes.DAL.Repositories
{
    public class ProcedureGroupRepository : RepositoryBase<ProcedureGroup>
    {
        public ProcedureGroupRepository(DataContext dc) : base(dc)
        {
        }
    }

}
