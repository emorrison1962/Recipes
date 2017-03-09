namespace Recipes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _030817d : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShoppingLists",
                c => new
                    {
                        ShoppingListId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ShoppingListId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ShoppingLists");
        }
    }
}
