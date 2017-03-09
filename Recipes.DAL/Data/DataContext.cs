using Recipes.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.DAL.Data
{
	public class DataContext : DbContext
	{
		public DataContext() : base("DefaultConnection")
		{

		}
		public DbSet<Recipe> Recipes { get; set; }
		public DbSet<Tag> Tags { get; set; }
        public DbSet<Ethnicity> Ethnicities { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Recipe>()
						.HasMany<Tag>(s => s.Tags)
						.WithMany(c => c.Recipes)
						.Map(cs =>
						{
							cs.MapLeftKey("RecipeRefId");
							cs.MapRightKey("TagRefId");
							cs.ToTable("RecipeTag");
						});
		}
	}//class
}//ns
