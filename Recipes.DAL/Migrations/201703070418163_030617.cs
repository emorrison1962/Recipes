namespace Recipes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _030617 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IngredientGroups",
                c => new
                    {
                        IngredientGroupId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Recipe_RecipeId = c.Int(),
                    })
                .PrimaryKey(t => t.IngredientGroupId)
                .ForeignKey("dbo.Recipes", t => t.Recipe_RecipeId)
                .Index(t => t.Recipe_RecipeId);
            
            CreateTable(
                "dbo.IngredientGroupItems",
                c => new
                    {
                        IngredientGroupItemId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Product_Text = c.String(),
                        IngredientGroup_IngredientGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.IngredientGroupItemId)
                .ForeignKey("dbo.IngredientGroups", t => t.IngredientGroup_IngredientGroupId)
                .Index(t => t.IngredientGroup_IngredientGroupId);
            
            CreateTable(
                "dbo.ProcedureGroups",
                c => new
                    {
                        ProcedureGroupId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Recipe_RecipeId = c.Int(),
                    })
                .PrimaryKey(t => t.ProcedureGroupId)
                .ForeignKey("dbo.Recipes", t => t.Recipe_RecipeId)
                .Index(t => t.Recipe_RecipeId);
            
            CreateTable(
                "dbo.ProcedureGroupItems",
                c => new
                    {
                        ProcedureGroupItemId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        ProcedureGroup_ProcedureGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.ProcedureGroupItemId)
                .ForeignKey("dbo.ProcedureGroups", t => t.ProcedureGroup_ProcedureGroupId)
                .Index(t => t.ProcedureGroup_ProcedureGroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProcedureGroups", "Recipe_RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.ProcedureGroupItems", "ProcedureGroup_ProcedureGroupId", "dbo.ProcedureGroups");
            DropForeignKey("dbo.IngredientGroups", "Recipe_RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.IngredientGroupItems", "IngredientGroup_IngredientGroupId", "dbo.IngredientGroups");
            DropIndex("dbo.ProcedureGroupItems", new[] { "ProcedureGroup_ProcedureGroupId" });
            DropIndex("dbo.ProcedureGroups", new[] { "Recipe_RecipeId" });
            DropIndex("dbo.IngredientGroupItems", new[] { "IngredientGroup_IngredientGroupId" });
            DropIndex("dbo.IngredientGroups", new[] { "Recipe_RecipeId" });
            DropTable("dbo.ProcedureGroupItems");
            DropTable("dbo.ProcedureGroups");
            DropTable("dbo.IngredientGroupItems");
            DropTable("dbo.IngredientGroups");
        }
    }
}
