﻿using Recipes.Domain;
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
        public DbSet<IngredientGroup> IngredientGroups { get; set; }
        public DbSet<IngredientGroupItem> IngredientGroupItems { get; set; }
        public DbSet<ProcedureGroup> ProcedureGroups { get; set; }
        public DbSet<ProcedureGroupItem> ProcedureGroupItems { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Recipe>()
						.HasMany<Tag>(r => r.Tags)
						.WithMany(t => t.Recipes)
						.Map(rt =>
						{
							rt.MapLeftKey("RecipeRefId");
							rt.MapRightKey("TagRefId");
							rt.ToTable("RecipeTag");
						});

            modelBuilder.Entity<ShoppingList>()
                        .HasMany<IngredientGroupItem>(sl => sl.Items)
                        .WithMany(igi => igi.ShoppingLists)
                        .Map(cs =>
                        {
                            cs.MapLeftKey("ShoppingListRefId");
                            cs.MapRightKey("IngredientGroupItemRefId");
                            cs.ToTable("ShoppingListIngredientGroupItem");
                        });

            modelBuilder.Entity<IngredientGroupItem>()
                                .HasOptional<IngredientGroup>(s => s.IngredientGroup)
                                .WithMany(s => s.Items);

            modelBuilder.Entity<ProcedureGroupItem>()
                                .HasOptional<ProcedureGroup>(s => s.ProcedureGroup)
                                .WithMany(s => s.Items);


#if false
            // Configure the primary Key for the OfficeAssignment 
            modelBuilder.Entity<OfficeAssignment>()
                .HasKey(t => t.InstructorID);

            modelBuilder.Entity<Instructor>()
                .HasRequired(t => t.OfficeAssignment)
                .WithRequiredPrincipal();

#endif
        }
    }//class
}//ns
