using Recipes.Domain;
using System.Data.Entity;

namespace Recipes.DAL.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

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
        //public DbSet<IngredientItem> ShoppingListItems { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>()
                        .HasMany<Tag>(r => r.Tags)
                        .WithMany(t => t.Recipes)
                        .Map(rt =>
                        {
                            rt.MapLeftKey("RecipeId");
                            rt.MapRightKey("TagId");
                            rt.ToTable("RecipeTag");
                        });

            modelBuilder.Entity<ShoppingList>()
                        .HasMany<ShoppingListGroup>(x => x.Groups);


            modelBuilder.Entity<ShoppingListItem>()
                                .HasOptional<ShoppingListGroup>(s => s.ShoppingListGroup)
                                .WithMany(s => s.Items);

            modelBuilder.Entity<IngredientItem>()
                                .HasOptional<IngredientGroup>(s => s.IngredientGroup)
                                .WithMany(s => s.Items);

            modelBuilder.Entity<ProcedureItem>()
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
