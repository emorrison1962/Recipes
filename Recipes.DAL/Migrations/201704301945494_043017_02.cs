namespace Recipes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _043017_02 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.IngredientGroups", "EntityKey_EntitySetName");
            DropColumn("dbo.IngredientGroups", "EntityKey_EntityContainerName");
            DropColumn("dbo.IngredientItems", "EntityKey_EntitySetName");
            DropColumn("dbo.IngredientItems", "EntityKey_EntityContainerName");
            DropColumn("dbo.PlannerGroups", "EntityKey_EntitySetName");
            DropColumn("dbo.PlannerGroups", "EntityKey_EntityContainerName");
            DropColumn("dbo.PlannerItems", "EntityKey_EntitySetName");
            DropColumn("dbo.PlannerItems", "EntityKey_EntityContainerName");
            DropColumn("dbo.ProcedureGroups", "EntityKey_EntitySetName");
            DropColumn("dbo.ProcedureGroups", "EntityKey_EntityContainerName");
            DropColumn("dbo.ProcedureItems", "EntityKey_EntitySetName");
            DropColumn("dbo.ProcedureItems", "EntityKey_EntityContainerName");
            DropColumn("dbo.Planners", "EntityKey_EntitySetName");
            DropColumn("dbo.Planners", "EntityKey_EntityContainerName");
            DropColumn("dbo.ShoppingListGroups", "EntityKey_EntitySetName");
            DropColumn("dbo.ShoppingListGroups", "EntityKey_EntityContainerName");
            DropColumn("dbo.ShoppingListItems", "EntityKey_EntitySetName");
            DropColumn("dbo.ShoppingListItems", "EntityKey_EntityContainerName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShoppingListItems", "EntityKey_EntityContainerName", c => c.String());
            AddColumn("dbo.ShoppingListItems", "EntityKey_EntitySetName", c => c.String());
            AddColumn("dbo.ShoppingListGroups", "EntityKey_EntityContainerName", c => c.String());
            AddColumn("dbo.ShoppingListGroups", "EntityKey_EntitySetName", c => c.String());
            AddColumn("dbo.Planners", "EntityKey_EntityContainerName", c => c.String());
            AddColumn("dbo.Planners", "EntityKey_EntitySetName", c => c.String());
            AddColumn("dbo.ProcedureItems", "EntityKey_EntityContainerName", c => c.String());
            AddColumn("dbo.ProcedureItems", "EntityKey_EntitySetName", c => c.String());
            AddColumn("dbo.ProcedureGroups", "EntityKey_EntityContainerName", c => c.String());
            AddColumn("dbo.ProcedureGroups", "EntityKey_EntitySetName", c => c.String());
            AddColumn("dbo.PlannerItems", "EntityKey_EntityContainerName", c => c.String());
            AddColumn("dbo.PlannerItems", "EntityKey_EntitySetName", c => c.String());
            AddColumn("dbo.PlannerGroups", "EntityKey_EntityContainerName", c => c.String());
            AddColumn("dbo.PlannerGroups", "EntityKey_EntitySetName", c => c.String());
            AddColumn("dbo.IngredientItems", "EntityKey_EntityContainerName", c => c.String());
            AddColumn("dbo.IngredientItems", "EntityKey_EntitySetName", c => c.String());
            AddColumn("dbo.IngredientGroups", "EntityKey_EntityContainerName", c => c.String());
            AddColumn("dbo.IngredientGroups", "EntityKey_EntitySetName", c => c.String());
        }
    }
}
