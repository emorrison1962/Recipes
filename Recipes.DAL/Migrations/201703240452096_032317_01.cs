namespace Recipes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _032317_01 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShoppingListGroups", "ShoppingList_ShoppingListId", "dbo.ShoppingLists");
            DropIndex("dbo.ShoppingListGroups", new[] { "ShoppingList_ShoppingListId" });
            AlterColumn("dbo.ShoppingListGroups", "ShoppingList_ShoppingListId", c => c.Int(nullable: false));
            CreateIndex("dbo.ShoppingListGroups", "ShoppingList_ShoppingListId");
            AddForeignKey("dbo.ShoppingListGroups", "ShoppingList_ShoppingListId", "dbo.ShoppingLists", "ShoppingListId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingListGroups", "ShoppingList_ShoppingListId", "dbo.ShoppingLists");
            DropIndex("dbo.ShoppingListGroups", new[] { "ShoppingList_ShoppingListId" });
            AlterColumn("dbo.ShoppingListGroups", "ShoppingList_ShoppingListId", c => c.Int());
            CreateIndex("dbo.ShoppingListGroups", "ShoppingList_ShoppingListId");
            AddForeignKey("dbo.ShoppingListGroups", "ShoppingList_ShoppingListId", "dbo.ShoppingLists", "ShoppingListId");
        }
    }
}
