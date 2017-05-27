namespace Recipes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _052317_02 : DbMigration
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
                        IngredientGroup_IngredientGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IngredientItemId)
                .ForeignKey("dbo.IngredientGroups", t => t.IngredientGroup_IngredientGroupId, cascadeDelete: true)
                .Index(t => t.IngredientGroup_IngredientGroupId);
            
            CreateTable(
                "dbo.PlannerGroups",
                c => new
                    {
                        PlannerGroupId = c.Int(nullable: false, identity: true),
                        Weekday = c.Int(nullable: false),
                        PlannerId = c.Int(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.PlannerGroupId)
                .ForeignKey("dbo.Planners", t => t.PlannerId, cascadeDelete: true)
                .Index(t => t.PlannerId);
            
            CreateTable(
                "dbo.PlannerItems",
                c => new
                    {
                        PlannerItemId = c.Int(nullable: false, identity: true),
                        RecipeId = c.Int(nullable: false),
                        PlannerGroupId = c.Int(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.PlannerItemId)
                .ForeignKey("dbo.PlannerGroups", t => t.PlannerGroupId, cascadeDelete: true)
                .ForeignKey("dbo.Recipes", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.RecipeId)
                .Index(t => t.PlannerGroupId);
            
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
                        ProcedureGroup_ProcedureGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProcedureItemId)
                .ForeignKey("dbo.ProcedureGroups", t => t.ProcedureGroup_ProcedureGroupId, cascadeDelete: true)
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
                "dbo.Planners",
                c => new
                    {
                        PlannerId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.PlannerId);
            
            CreateTable(
                "dbo.ShoppingListGroups",
                c => new
                    {
                        ShoppingListGroupId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        ShoppingList_ShoppingListId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ShoppingListGroupId)
                .ForeignKey("dbo.ShoppingLists", t => t.ShoppingList_ShoppingListId, cascadeDelete: true)
                .Index(t => t.ShoppingList_ShoppingListId);
            
            CreateTable(
                "dbo.ShoppingListItems",
                c => new
                    {
                        ShoppingListItemId = c.Int(nullable: false, identity: true),
                        IsChecked = c.Boolean(nullable: false),
                        Text = c.String(),
                        ShoppingListGroup_ShoppingListGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ShoppingListItemId)
                .ForeignKey("dbo.ShoppingListGroups", t => t.ShoppingListGroup_ShoppingListGroupId, cascadeDelete: true)
                .Index(t => t.ShoppingListGroup_ShoppingListGroupId);
            
            CreateTable(
                "dbo.ShoppingLists",
                c => new
                    {
                        ShoppingListId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.ShoppingListId);
            
            CreateTable(
                "dbo.Weekdays",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 32),
                        Description = c.String(maxLength: 32),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Recipe2Tag",
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
            DropForeignKey("dbo.ShoppingListItems", "ShoppingListGroup_ShoppingListGroupId", "dbo.ShoppingListGroups");
            DropForeignKey("dbo.PlannerGroups", "PlannerId", "dbo.Planners");
            DropForeignKey("dbo.PlannerItems", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.Recipe2Tag", "TagId", "dbo.Tags");
            DropForeignKey("dbo.Recipe2Tag", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.ProcedureGroups", "Recipe_RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.ProcedureItems", "ProcedureGroup_ProcedureGroupId", "dbo.ProcedureGroups");
            DropForeignKey("dbo.IngredientGroups", "Recipe_RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.PlannerItems", "PlannerGroupId", "dbo.PlannerGroups");
            DropForeignKey("dbo.IngredientItems", "IngredientGroup_IngredientGroupId", "dbo.IngredientGroups");
            DropIndex("dbo.Recipe2Tag", new[] { "TagId" });
            DropIndex("dbo.Recipe2Tag", new[] { "RecipeId" });
            DropIndex("dbo.ShoppingListItems", new[] { "ShoppingListGroup_ShoppingListGroupId" });
            DropIndex("dbo.ShoppingListGroups", new[] { "ShoppingList_ShoppingListId" });
            DropIndex("dbo.ProcedureItems", new[] { "ProcedureGroup_ProcedureGroupId" });
            DropIndex("dbo.ProcedureGroups", new[] { "Recipe_RecipeId" });
            DropIndex("dbo.PlannerItems", new[] { "PlannerGroupId" });
            DropIndex("dbo.PlannerItems", new[] { "RecipeId" });
            DropIndex("dbo.PlannerGroups", new[] { "PlannerId" });
            DropIndex("dbo.IngredientItems", new[] { "IngredientGroup_IngredientGroupId" });
            DropIndex("dbo.IngredientGroups", new[] { "Recipe_RecipeId" });
            DropTable("dbo.Recipe2Tag");
            DropTable("dbo.Weekdays");
            DropTable("dbo.ShoppingLists");
            DropTable("dbo.ShoppingListItems");
            DropTable("dbo.ShoppingListGroups");
            DropTable("dbo.Planners");
            DropTable("dbo.Tags");
            DropTable("dbo.ProcedureItems");
            DropTable("dbo.ProcedureGroups");
            DropTable("dbo.Recipes");
            DropTable("dbo.PlannerItems");
            DropTable("dbo.PlannerGroups");
            DropTable("dbo.IngredientItems");
            DropTable("dbo.IngredientGroups");
            DropTable("dbo.Ethnicities");
        }
    }
}
