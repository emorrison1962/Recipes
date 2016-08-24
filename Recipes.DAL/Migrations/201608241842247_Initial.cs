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
                        TagId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Recipe_RecipeId = c.Int(),
                    })
                .PrimaryKey(t => t.TagId)
                .ForeignKey("dbo.Recipes", t => t.Recipe_RecipeId)
                .Index(t => t.Recipe_RecipeId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "Recipe_RecipeId", "dbo.Recipes");
            DropIndex("dbo.Categories", new[] { "Recipe_RecipeId" });
            DropTable("dbo.Recipes");
            DropTable("dbo.Ethnicities");
            DropTable("dbo.Categories");
        }
    }
}
