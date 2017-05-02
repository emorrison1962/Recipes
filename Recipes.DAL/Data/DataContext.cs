﻿using Recipes.Domain;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;

namespace Recipes.DAL.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }

        public void SetChanges(EntityDeltaResults ar)
        {
            foreach (var delta in ar.Deltas)
            {
                if (delta.EntityState == System.Data.EntityState.Added)
                {
                    var dbEntry = this.Entry(delta.Entity);
                    dbEntry.State = EntityState.Added;
                }
                else if (delta.EntityState == System.Data.EntityState.Deleted)
                {
                    var dbEntry = this.Entry(delta.Entity);
                    dbEntry.State = EntityState.Deleted;
                }
                else if (delta.EntityState == System.Data.EntityState.Modified)
                {
                    var dbEntry = this.Entry(delta.Entity);
                    dbEntry.State = EntityState.Modified;
                }
                else
                {
                    new Object();
                }
            }
        }


        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<Ethnicity> Ethnicities { get; set; }

        public DbSet<IngredientGroup> IngredientGroups { get; set; }
        public DbSet<IngredientItem> IngredientItems { get; set; }
        public DbSet<ProcedureGroup> ProcedureGroups { get; set; }
        public DbSet<ProcedureItem> ProcedureItems { get; set; }

        public DbSet<ShoppingListGroup> ShoppingListGroups { get; set; }
        public DbSet<ShoppingListItem> ShoppingListItems { get; set; }

        public DbSet<PlannerItem> PlannerItems { get; set; }
        public DbSet<PlannerGroup> PlannerGroups { get; set; }
        public DbSet<Planner> Planners { get; set; }
        public DbSet<Weekday> Weekdays { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>()
                        .HasMany<Tag>(r => r.Tags)
                        .WithMany(t => t.Recipes)
                        .Map(rt =>
                        {
                            rt.MapLeftKey("RecipeId");
                            rt.MapRightKey("TagId");
                            rt.ToTable("Recipe2Tag");
                        });

            //modelBuilder.Entity<Recipe>().Property(x => x.Uri)
            //    .HasColumnAnnotation("Constraint", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));


            ///////////////////////////////////////////////////////////////////////////////////
            modelBuilder.Entity<ShoppingListItem>()
                        .HasRequired<ShoppingListGroup>(s => s.ShoppingListGroup)
                        .WithMany(s => s.Items);

            modelBuilder.Entity<ShoppingListGroup>()
                        .HasMany<ShoppingListItem>(x => x.Items);
            modelBuilder.Entity<ShoppingListGroup>()
                        .HasRequired<ShoppingList>(x => x.ShoppingList);

            modelBuilder.Entity<ShoppingList>()
                        .HasMany<ShoppingListGroup>(x => x.Groups);
            ///////////////////////////////////////////////////////////////////////////////////

            modelBuilder.Entity<IngredientItem>()
                                .HasRequired<IngredientGroup>(s => s.IngredientGroup)
                                .WithMany(s => s.Items);

            modelBuilder.Entity<ProcedureItem>()
                                .HasRequired<ProcedureGroup>(s => s.ProcedureGroup)
                                .WithMany(s => s.Items);
            ///////////////////////////////////////////////////////////////////////////////////

            modelBuilder.Entity<PlannerItem>()
                                .HasRequired<Recipe>(x => x.Recipe);

            modelBuilder.Entity<PlannerItem>()
                                .HasRequired<PlannerGroup>(x => x.PlannerGroup)
                                .WithMany(s => s.Items)
                                .HasForeignKey(x => x.PlannerGroupId);

            modelBuilder.Entity<PlannerGroup>()
                                .HasMany<PlannerItem>(x => x.Items);

            modelBuilder.Entity<PlannerGroup>()
                                .HasRequired<Planner>(x => x.Planner);

            modelBuilder.Entity<Planner>()
                                .HasMany<PlannerGroup>(x => x.Groups);

            ///////////////////////////////////////////////////////////////////////////////////

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
