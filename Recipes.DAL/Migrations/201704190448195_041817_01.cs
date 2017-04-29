namespace Recipes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _041817_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IngredientGroups", "IsDetached", c => c.Boolean(nullable: false));
            AddColumn("dbo.IngredientItems", "IsDetached", c => c.Boolean(nullable: false));
            AddColumn("dbo.PlannerGroups", "IsDetached", c => c.Boolean(nullable: false));
            AddColumn("dbo.PlannerItems", "IsDetached", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProcedureGroups", "IsDetached", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProcedureItems", "IsDetached", c => c.Boolean(nullable: false));
            AddColumn("dbo.Planners", "IsDetached", c => c.Boolean(nullable: false));
            AddColumn("dbo.ShoppingListGroups", "IsDetached", c => c.Boolean(nullable: false));
            AddColumn("dbo.ShoppingListItems", "IsDetached", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShoppingListItems", "IsDetached");
            DropColumn("dbo.ShoppingListGroups", "IsDetached");
            DropColumn("dbo.Planners", "IsDetached");
            DropColumn("dbo.ProcedureItems", "IsDetached");
            DropColumn("dbo.ProcedureGroups", "IsDetached");
            DropColumn("dbo.PlannerItems", "IsDetached");
            DropColumn("dbo.PlannerGroups", "IsDetached");
            DropColumn("dbo.IngredientItems", "IsDetached");
            DropColumn("dbo.IngredientGroups", "IsDetached");
        }
    }
}
