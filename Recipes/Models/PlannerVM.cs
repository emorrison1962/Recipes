using System.Collections.Generic;
using Recipes.Domain;
using System.Linq;

namespace Recipes.Models
{
    internal class PlannerVM
    {
        public Planner Planner { get; set; }
        public List<Recipe> RecipeCatalog { get; set; }

        public PlannerVM(Planner planner, IEnumerable<Recipe> recipes)
        {
            this.Planner = planner;
            this.RecipeCatalog = recipes.ToList();
        }
    }
}