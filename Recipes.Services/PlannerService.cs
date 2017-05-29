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
    public class PlannerService : ServiceBase<Planner>
    {
        IServiceBase<PlannerGroup> GroupService { get; set; }
        public PlannerService(IRepositoryBase<Planner> repository, IServiceBase<PlannerGroup> groupSvc)
            : base(repository)
        {
            this.GroupService = groupSvc;
        }

        public override Planner Insert(Planner entity)
        {
            throw new NotSupportedException("Planner is a singleton.");
        }
    }//class

    public class PlannerGroupService : ServiceBase<PlannerGroup>
    {
        public PlannerGroupService(IRepositoryBase<PlannerGroup> repository, IServiceBase<PlannerItem> itemSvc)
            : base(repository)
        {
        }

    }//class

    public class PlannerItemService : ServiceBase<PlannerItem>
    {
        public PlannerItemService(IRepositoryBase<PlannerItem> repository)
            : base(repository)
        {
        }

    }//class
}//ns
