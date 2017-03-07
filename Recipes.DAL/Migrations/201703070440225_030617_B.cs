namespace Recipes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _030617_B : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.IngredientGroupItems", "Product_Text");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IngredientGroupItems", "Product_Text", c => c.String());
        }
    }
}
