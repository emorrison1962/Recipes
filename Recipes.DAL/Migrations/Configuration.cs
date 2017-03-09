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
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Recipes.DAL.Data.DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            this.InsertTags(context);
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
