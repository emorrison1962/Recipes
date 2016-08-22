namespace Recipes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
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
                        Source = c.String(nullable: true),
                        CategoryId = c.Int(nullable: true),
                        EthnicityId = c.Int(nullable: true),
                        Rating = c.Int(nullable: true),
                        Time = c.Time(nullable: true, precision: 7),
                    })
                .PrimaryKey(t => t.RecipeId);
            
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        SubCategoryId = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.SubCategoryId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SubCategories");
            DropTable("dbo.Recipes");
            DropTable("dbo.Ethnicities");
            DropTable("dbo.Categories");
        }
    }
}
