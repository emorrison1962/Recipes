using Recipes.Dal.Repositories;
using Recipes.DAL.Data;
using Recipes.Domain;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;

namespace Recipes.DAL.Repositories
{
    #region PlannerRepository

    public class PlannerRepository : RepositoryBase<Planner>
    {
        public PlannerRepository(DataContext dc) : base(dc)
        {

        }

        public override Planner GetById(int id)
        {
            var result = this._dataContext.Planners
                .Include(p => p.Groups.Select(g => g.Items))
                .FirstOrDefault();
            return result;
        }

        public override void Update(Planner pending)
        {
            try
            {
                var existing = this.GetById(pending.PlannerId);
                if (!existing.Equals(pending))
                {
                    var ecr = existing.DetectChanges(pending);
                    _dataContext.SetChanges(ecr);
                    _dataContext.SaveChanges();
                }
            }
#pragma warning disable 0168
            catch (Exception ex)
            {
                throw;
            }
#pragma warning restore 0168
        }



    }//class

    #endregion

    #region PlannerGroupRepository

    public class PlannerGroupRepository : RepositoryBase<PlannerGroup>
    {
        public PlannerGroupRepository(DataContext dc) : base(dc)
        {
        }
    }//class

    #endregion

    #region PlannerItemRepository

    public class PlannerItemRepository : RepositoryBase<PlannerItem>
    {
        public PlannerItemRepository(DataContext dc) : base(dc)
        {
        }
    }//class

    #endregion

}
