namespace Recipes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _03151701 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IngredientGroupItems", "ShoppingList_ShoppingListId", "dbo.ShoppingLists");
            DropIndex("dbo.IngredientGroupItems", new[] { "ShoppingList_ShoppingListId" });
            CreateTable(
                "dbo.ShoppingListIngredientGroupItem",
                c => new
                    {
                        ShoppingListRefId = c.Int(nullable: false),
                        IngredientGroupItemRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ShoppingListRefId, t.IngredientGroupItemRefId })
                .ForeignKey("dbo.ShoppingLists", t => t.ShoppingListRefId, cascadeDelete: true)
                .ForeignKey("dbo.IngredientGroupItems", t => t.IngredientGroupItemRefId, cascadeDelete: true)
                .Index(t => t.ShoppingListRefId)
                .Index(t => t.IngredientGroupItemRefId);
            
            DropColumn("dbo.IngredientGroupItems", "ShoppingList_ShoppingListId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IngredientGroupItems", "ShoppingList_ShoppingListId", c => c.Int());
            DropForeignKey("dbo.ShoppingListIngredientGroupItem", "IngredientGroupItemRefId", "dbo.IngredientGroupItems");
            DropForeignKey("dbo.ShoppingListIngredientGroupItem", "ShoppingListRefId", "dbo.ShoppingLists");
            DropIndex("dbo.ShoppingListIngredientGroupItem", new[] { "IngredientGroupItemRefId" });
            DropIndex("dbo.ShoppingListIngredientGroupItem", new[] { "ShoppingListRefId" });
            DropTable("dbo.ShoppingListIngredientGroupItem");
            CreateIndex("dbo.IngredientGroupItems", "ShoppingList_ShoppingListId");
            AddForeignKey("dbo.IngredientGroupItems", "ShoppingList_ShoppingListId", "dbo.ShoppingLists", "ShoppingListId");
        }
    }
}
