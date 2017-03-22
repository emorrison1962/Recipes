namespace Recipes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _032117_01 : DbMigration
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
                "dbo.IngredientItems",
                c => new
                    {
                        IngredientItemId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        IngredientGroup_IngredientGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.IngredientItemId)
                .ForeignKey("dbo.IngredientGroups", t => t.IngredientGroup_IngredientGroupId)
                .Index(t => t.IngredientGroup_IngredientGroupId);
            
            CreateTable(
                "dbo.ShoppingListItems",
                c => new
                    {
                        ShoppingListItemId = c.Int(nullable: false),
                        IsChecked = c.Boolean(nullable: false),
                        Text = c.String(),
                        ShoppingListGroup_ShoppingListGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.ShoppingListItemId)
                .ForeignKey("dbo.IngredientItems", t => t.ShoppingListItemId)
                .ForeignKey("dbo.ShoppingListGroups", t => t.ShoppingListGroup_ShoppingListGroupId)
                .Index(t => t.ShoppingListItemId)
                .Index(t => t.ShoppingListGroup_ShoppingListGroupId);
            
            CreateTable(
                "dbo.ShoppingListGroups",
                c => new
                    {
                        ShoppingListGroupId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        ShoppingList_ShoppingListId = c.Int(),
                    })
                .PrimaryKey(t => t.ShoppingListGroupId)
                .ForeignKey("dbo.ShoppingLists", t => t.ShoppingList_ShoppingListId)
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
                "dbo.ProcedureItems",
                c => new
                    {
                        ProcedureItemId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        ProcedureGroup_ProcedureGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.ProcedureItemId)
                .ForeignKey("dbo.ProcedureGroups", t => t.ProcedureGroup_ProcedureGroupId)
                .Index(t => t.ProcedureGroup_ProcedureGroupId);
            
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
                        RecipeId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RecipeId, t.TagId })
                .ForeignKey("dbo.Recipes", t => t.RecipeId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.RecipeId)
                .Index(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingListGroups", "ShoppingList_ShoppingListId", "dbo.ShoppingLists");
            DropForeignKey("dbo.RecipeTag", "TagId", "dbo.Tags");
            DropForeignKey("dbo.RecipeTag", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.ProcedureGroups", "Recipe_RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.IngredientGroups", "Recipe_RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.ProcedureItems", "ProcedureGroup_ProcedureGroupId", "dbo.ProcedureGroups");
            DropForeignKey("dbo.ShoppingListItems", "ShoppingListGroup_ShoppingListGroupId", "dbo.ShoppingListGroups");
            DropForeignKey("dbo.ShoppingListItems", "ShoppingListItemId", "dbo.IngredientItems");
            DropForeignKey("dbo.IngredientItems", "IngredientGroup_IngredientGroupId", "dbo.IngredientGroups");
            DropIndex("dbo.RecipeTag", new[] { "TagId" });
            DropIndex("dbo.RecipeTag", new[] { "RecipeId" });
            DropIndex("dbo.ProcedureItems", new[] { "ProcedureGroup_ProcedureGroupId" });
            DropIndex("dbo.ProcedureGroups", new[] { "Recipe_RecipeId" });
            DropIndex("dbo.ShoppingListGroups", new[] { "ShoppingList_ShoppingListId" });
            DropIndex("dbo.ShoppingListItems", new[] { "ShoppingListGroup_ShoppingListGroupId" });
            DropIndex("dbo.ShoppingListItems", new[] { "ShoppingListItemId" });
            DropIndex("dbo.IngredientItems", new[] { "IngredientGroup_IngredientGroupId" });
            DropIndex("dbo.IngredientGroups", new[] { "Recipe_RecipeId" });
            DropTable("dbo.RecipeTag");
            DropTable("dbo.ShoppingLists");
            DropTable("dbo.Tags");
            DropTable("dbo.Recipes");
            DropTable("dbo.ProcedureItems");
            DropTable("dbo.ProcedureGroups");
            DropTable("dbo.ShoppingListGroups");
            DropTable("dbo.ShoppingListItems");
            DropTable("dbo.IngredientItems");
            DropTable("dbo.IngredientGroups");
            DropTable("dbo.Ethnicities");
        }
    }
}
