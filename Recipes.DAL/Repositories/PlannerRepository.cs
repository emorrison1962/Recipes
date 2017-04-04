using Recipes.Dal.Repositories;
using Recipes.DAL.Data;
using Recipes.Domain;

namespace Recipes.DAL.Repositories
{
    public class PlannerRepository : RepositoryBase<Planner>
    {
        public PlannerRepository(DataContext dc) : base(dc)
        {

        }
    }

    public class PlannerGroupRepository : RepositoryBase<PlannerGroup>
    {
        public PlannerGroupRepository(DataContext dc) : base(dc)
        {

        }
    }

    public class PlannerItemRepository : RepositoryBase<PlannerItem>
    {
        public PlannerItemRepository(DataContext dc) : base(dc)
        {

        }
    }

}
