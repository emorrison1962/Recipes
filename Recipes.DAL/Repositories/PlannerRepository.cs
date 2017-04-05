using Recipes.Dal.Repositories;
using Recipes.DAL.Data;
using Recipes.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Recipes.DAL.Repositories
{
    public class PlannerRepository : RepositoryBase<Planner>
    {
        public PlannerRepository(DataContext dc) : base(dc)
        {

        }

        public override Planner GetById(int id)
        {
            var query = this._dbSet
                .Where(x => null != x)
                .IncludeMultiple(
                    p => p.Groups
                    , p => p.Groups.Select<PlannerGroup, List<PlannerItem>>(pg => pg.Items));

            var result = query.FirstOrDefault();
            return result;
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
