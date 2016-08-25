namespace Recipes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _082416 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "Recipe_RecipeId", "dbo.Recipes");
            DropIndex("dbo.Categories", new[] { "Recipe_RecipeId" });
            CreateTable(
                "dbo.RecipeTags",
                c => new
                    {
                        Recipe_RecipeId = c.Int(nullable: false),
                        Tag_TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Recipe_RecipeId, t.Tag_TagId })
                .ForeignKey("dbo.Recipes", t => t.Recipe_RecipeId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Tag_TagId, cascadeDelete: true)
                .Index(t => t.Recipe_RecipeId)
                .Index(t => t.Tag_TagId);
            
            DropColumn("dbo.Categories", "Recipe_RecipeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Recipe_RecipeId", c => c.Int());
            DropForeignKey("dbo.RecipeTags", "Tag_TagId", "dbo.Categories");
            DropForeignKey("dbo.RecipeTags", "Recipe_RecipeId", "dbo.Recipes");
            DropIndex("dbo.RecipeTags", new[] { "Tag_TagId" });
            DropIndex("dbo.RecipeTags", new[] { "Recipe_RecipeId" });
            DropTable("dbo.RecipeTags");
            CreateIndex("dbo.Categories", "Recipe_RecipeId");
            AddForeignKey("dbo.Categories", "Recipe_RecipeId", "dbo.Recipes", "RecipeId");
        }
    }
}
