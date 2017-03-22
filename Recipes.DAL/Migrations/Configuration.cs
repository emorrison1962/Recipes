namespace Recipes.DAL.Migrations
{
    using Domain;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Recipes.DAL.Data.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Recipes.DAL.Data.DataContext context)
        {
            this.SeedShoppingList(context);
            this.InsertTags(context);
        }

        void SeedShoppingList(Recipes.DAL.Data.DataContext context)
        {
            var groups = new ShoppingListGroup[] {
                new ShoppingListGroup { Text = "<Unknown>" },
                new ShoppingListGroup { Text = "Produce" },
                new ShoppingListGroup { Text = "Meat" },
                new ShoppingListGroup { Text = "Dairy" },
                new ShoppingListGroup { Text = "Deli" },
                new ShoppingListGroup { Text = "Soap" },
                new ShoppingListGroup { Text = "Paper" },
                new ShoppingListGroup { Text = "Sam's" },
                new ShoppingListGroup { Text = "Other" }};
            context.ShoppingListGroups.AddOrUpdate(t => t.Text,groups);

            var sl = new ShoppingList();
            context.ShoppingLists.AddOrUpdate(sl);
            sl.Groups.AddRange(groups);
            context.SaveChanges();
        }

        void InsertTags(Recipes.DAL.Data.DataContext context)
        {
            context.Tags.AddOrUpdate(t => t.Name,
                new Tag { Name = "Chicken" },
                new Tag { Name = "Beef" },
                new Tag { Name = "Pork" },
                new Tag { Name = "Seafood" },
                new Tag { Name = "Vegetable" },
                new Tag { Name = "Dessert" },
                new Tag { Name = "Ice Cream" });
        }
    }
}
