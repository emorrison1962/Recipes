namespace Recipes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _030917 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ethnicities",
                c => new
                    {
                        EthnicityId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.EthnicityId);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        RecipeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Uri = c.String(),
                        Source = c.String(),
                        EthnicityId = c.Int(),
                        Rating = c.Int(),
                        Time = c.Time(precision: 7),
                        ImageUri = c.String(),
                    })
                .PrimaryKey(t => t.RecipeId);
            
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
                        IngredientGroup_IngredientGroupId = c.Int(),
                        ShoppingList_ShoppingListId = c.Int(),
                    })
                .PrimaryKey(t => t.IngredientGroupItemId)
                .ForeignKey("dbo.IngredientGroups", t => t.IngredientGroup_IngredientGroupId)
                .ForeignKey("dbo.ShoppingLists", t => t.ShoppingList_ShoppingListId)
                .Index(t => t.IngredientGroup_IngredientGroupId)
                .Index(t => t.ShoppingList_ShoppingListId);
            
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
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "dbo.ShoppingLists",
                c => new
                    {
                        ShoppingListId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ShoppingListId);
            
            CreateTable(
                "dbo.RecipeTag",
                c => new
                    {
                        RecipeRefId = c.Int(nullable: false),
                        TagRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RecipeRefId, t.TagRefId })
                .ForeignKey("dbo.Recipes", t => t.RecipeRefId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagRefId, cascadeDelete: true)
                .Index(t => t.RecipeRefId)
                .Index(t => t.TagRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IngredientGroupItems", "ShoppingList_ShoppingListId", "dbo.ShoppingLists");
            DropForeignKey("dbo.RecipeTag", "TagRefId", "dbo.Tags");
            DropForeignKey("dbo.RecipeTag", "RecipeRefId", "dbo.Recipes");
            DropForeignKey("dbo.ProcedureGroups", "Recipe_RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.ProcedureGroupItems", "ProcedureGroup_ProcedureGroupId", "dbo.ProcedureGroups");
            DropForeignKey("dbo.IngredientGroups", "Recipe_RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.IngredientGroupItems", "IngredientGroup_IngredientGroupId", "dbo.IngredientGroups");
            DropIndex("dbo.RecipeTag", new[] { "TagRefId" });
            DropIndex("dbo.RecipeTag", new[] { "RecipeRefId" });
            DropIndex("dbo.ProcedureGroupItems", new[] { "ProcedureGroup_ProcedureGroupId" });
            DropIndex("dbo.ProcedureGroups", new[] { "Recipe_RecipeId" });
            DropIndex("dbo.IngredientGroupItems", new[] { "ShoppingList_ShoppingListId" });
            DropIndex("dbo.IngredientGroupItems", new[] { "IngredientGroup_IngredientGroupId" });
            DropIndex("dbo.IngredientGroups", new[] { "Recipe_RecipeId" });
            DropTable("dbo.RecipeTag");
            DropTable("dbo.ShoppingLists");
            DropTable("dbo.Tags");
            DropTable("dbo.ProcedureGroupItems");
            DropTable("dbo.ProcedureGroups");
            DropTable("dbo.IngredientGroupItems");
            DropTable("dbo.IngredientGroups");
            DropTable("dbo.Recipes");
            DropTable("dbo.Ethnicities");
        }
    }
}
