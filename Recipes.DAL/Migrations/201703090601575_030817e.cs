namespace Recipes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _030817e : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IngredientGroupItems", "ShoppingList_ShoppingListId", c => c.Int());
            CreateIndex("dbo.IngredientGroupItems", "ShoppingList_ShoppingListId");
            AddForeignKey("dbo.IngredientGroupItems", "ShoppingList_ShoppingListId", "dbo.ShoppingLists", "ShoppingListId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IngredientGroupItems", "ShoppingList_ShoppingListId", "dbo.ShoppingLists");
            DropIndex("dbo.IngredientGroupItems", new[] { "ShoppingList_ShoppingListId" });
            DropColumn("dbo.IngredientGroupItems", "ShoppingList_ShoppingListId");
        }
    }
}
