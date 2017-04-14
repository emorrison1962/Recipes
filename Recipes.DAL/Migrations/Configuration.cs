namespace Recipes.DAL.Migrations
{
    using Data;
    using Domain;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //Database.SetInitializer<DataContext>(new CreateDatabaseIfNotExists<DataContext>());
            Database.SetInitializer<DataContext>(new DropCreateDatabaseIfModelChanges<DataContext>());
            //Database.SetInitializer<DataContext>(new DropCreateDatabaseAlways<DataContext>());
            //Database.SetInitializer<DataContext>(new SchoolDBInitializer());
        }


        protected override void Seed(Recipes.DAL.Data.DataContext context)
        {
            this.SeedShoppingList(context);
            this.SeedWeekdays(context);
            this.SeedPlanner(context);
        }

        void SeedShoppingList(Recipes.DAL.Data.DataContext context)
        {
            var shoppingList = new ShoppingList() { Text = "<Default>" };
            context.ShoppingLists.AddOrUpdate(x => x.Text);

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


            context.SaveChanges();
        }

        void SeedPlanner(DataContext context)
        {
            var planner = new Planner() { Text = "<Default>" };
            context.Planners.AddOrUpdate(x => x.Text);
            context.SaveChanges();

            var groups = new PlannerGroup[] {
                new PlannerGroup { Weekday = WeekdayEnum.Unknown, Planner = planner  },
                new PlannerGroup { Weekday = WeekdayEnum.Sunday, Planner = planner },
                new PlannerGroup { Weekday = WeekdayEnum.Monday, Planner = planner  },
                new PlannerGroup { Weekday = WeekdayEnum.Tuesday, Planner = planner  },
                new PlannerGroup { Weekday = WeekdayEnum.Wednesday, Planner = planner  },
                new PlannerGroup { Weekday = WeekdayEnum.Thursday, Planner = planner  },
                new PlannerGroup { Weekday = WeekdayEnum.Friday, Planner = planner  },
                new PlannerGroup { Weekday = WeekdayEnum.Saturday, Planner = planner  }};
            planner.Groups.AddRange(groups);

            //context.PlannerGroups.AddOrUpdate(t => t.Text, groups);
            context.SaveChanges();
        }
        void SeedWeekdays(DataContext context)
        {
            context.Weekdays.SeedEnumValues<Weekday, WeekdayEnum>(@enum => @enum);
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
    }//class

    public static class Extensions
    {
        public static void SeedEnumValues<T, TEnum>(this IDbSet<T> dbSet, Func<TEnum, T> converter)
        where T : class => Enum.GetValues(typeof(TEnum))
                               .Cast<object>()
                               .Select(value => converter((TEnum)value))
                               .ToList()
                               .ForEach(instance => dbSet.AddOrUpdate(instance));

    }//class
}//ns
