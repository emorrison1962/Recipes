namespace Recipes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _040317_06 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Planners", "Text", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Planners", "Text");
        }
    }
}
