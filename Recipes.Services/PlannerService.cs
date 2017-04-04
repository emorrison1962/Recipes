using Recipes.Contracts.Repositories;
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
        IRepositoryBase<PlannerItem> ItemRepository { get; set; }
        public PlannerService(IRepositoryBase<Planner> repository, IRepositoryBase<PlannerItem> itemRepo)
            : base(repository)
        {
            this.ItemRepository = itemRepo;
        }
    }//class

    public class PlannerGroupService : ServiceBase<PlannerGroup>
    {
        public PlannerGroupService(IRepositoryBase<PlannerGroup> repository)
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
