using Recipes.Dal.Repositories;
using Recipes.DAL.Data;
using Recipes.Domain;
using System;
using System.Data.Entity;
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


            //var query = this._dbSet
            //    .Where(x => null != x)
            //    .IncludeMultiple(
            //        p => p.Groups
            //        , p => p.Groups.Select<PlannerGroup, List<PlannerItem>>(pg => pg.Items));

            //var result = query.FirstOrDefault();
            return result;
        }

        public override void Update(Planner pending)
        {

            try
            {
                var existing = this.GetById(pending.PlannerId);

                if (!existing.Equals(pending))
                {
                    var ar = existing.DetectChanges(pending);
                    _dataContext.SetChanges(ar);
                    _dataContext.SaveChanges();
                }
            }

            catch (Exception ex)
            {
                throw;
            }
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
