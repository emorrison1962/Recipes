using Recipes.Dal.Repositories;
using Recipes.DAL.Data;
using Recipes.Domain;
using System;
using System.Collections.Generic;
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
            var query = this._dbSet
                .Where(x => null != x)
                .IncludeMultiple(
                    p => p.Groups
                    , p => p.Groups.Select<PlannerGroup, List<PlannerItem>>(pg => pg.Items));

            var result = query.FirstOrDefault();
            return result;
        }

        public override void Update(Planner entity)
        {

            try
            {
                Action<PlannerItem> clearRecipe = delegate (PlannerItem i) { i.RecipeId = i.Recipe.RecipeId; i.Recipe = null; };
                entity.Groups.ForEach(g => g.Items.ForEach(i => clearRecipe(i)));
                var existing = this.GetById(entity.PlannerId);

                if (!existing.Equals(entity))
                {
                    existing.Copy(entity);
                    var b = existing.Equals(entity);
                    var validations = _dataContext.GetValidationErrors();
                    foreach (var validation in validations)
                    {
                        Debug.WriteLine(validation.ToString());
                    }


                    _dataContext.SaveChanges();

                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }

    #endregion

    #region PlannerGroupRepository

    public class PlannerGroupRepository : RepositoryBase<PlannerGroup>
    {
        public PlannerGroupRepository(DataContext dc) : base(dc)
        {

        }
    }

    #endregion

    #region PlannerItemRepository

    public class PlannerItemRepository : RepositoryBase<PlannerItem>
    {
        public PlannerItemRepository(DataContext dc) : base(dc)
        {

        }
    }

    #endregion

}
