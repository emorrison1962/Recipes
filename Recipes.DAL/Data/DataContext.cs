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
		public DbSet<Tag> Categories { get; set; }
		public DbSet<Ethnicity> Ethnicities { get; set; }


	}
}
