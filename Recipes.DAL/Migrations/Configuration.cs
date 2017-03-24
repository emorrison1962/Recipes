namespace Recipes.DAL.Migrations
{
    using Data;
    using Domain;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            Database.SetInitializer<DataContext>(new CreateDatabaseIfNotExists<DataContext>());
            //Database.SetInitializer<DataContext>(new DropCreateDatabaseIfModelChanges<DataContext>());
            //Database.SetInitializer<DataContext>(new DropCreateDatabaseAlways<DataContext>());
            //Database.SetInitializer<DataContext>(new SchoolDBInitializer());
        }
    

        protected override void Seed(Recipes.DAL.Data.DataContext context)
        {
            this.SeedShoppingList(context);
            //this.InsertTags(context);
        }

        void SeedShoppingList(Recipes.DAL.Data.DataContext context)
        {
            var shoppingList = new ShoppingList() { Text = "<Default>" };
            context.ShoppingLists.AddOrUpdate(x => x.ShoppingListId);

            var groups = new ShoppingListGroup[] {
                new ShoppingListGroup { Text = "<Unknown>", ShoppingList = shoppingList },
                new ShoppingListGroup { Text = "Produce", ShoppingList = shoppingList  },
                new ShoppingListGroup { Text = "Meat", ShoppingList = shoppingList  },
                new ShoppingListGroup { Text = "Dairy", ShoppingList = shoppingList  },
                new ShoppingListGroup { Text = "Deli", ShoppingList = shoppingList  },
                new ShoppingListGroup { Text = "Soap", ShoppingList = shoppingList  },
                new ShoppingListGroup { Text = "Paper", ShoppingList = shoppingList  },
                new ShoppingListGroup { Text = "Sam's", ShoppingList = shoppingList  },
                new ShoppingListGroup { Text = "Other", ShoppingList = shoppingList  }};
            shoppingList.Groups.AddRange(groups);

            context.ShoppingListGroups.AddOrUpdate(t => t.Text, groups);


            //context.SaveChanges();
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
