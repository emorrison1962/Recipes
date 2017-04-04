namespace Recipes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _040317_01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlannerItems",
                c => new
                    {
                        PlannerItemId = c.Int(nullable: false, identity: true),
                        WeekDay = c.Int(nullable: false),
                        RecipeId = c.Int(nullable: false),
                        Planner_PlannerId = c.Int(),
                    })
                .PrimaryKey(t => t.PlannerItemId)
                .ForeignKey("dbo.Recipes", t => t.RecipeId, cascadeDelete: true)
                .ForeignKey("dbo.Planners", t => t.Planner_PlannerId)
                .Index(t => t.RecipeId)
                .Index(t => t.Planner_PlannerId);
            
            CreateTable(
                "dbo.Planners",
                c => new
                    {
                        PlannerId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.PlannerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlannerItems", "Planner_PlannerId", "dbo.Planners");
            DropForeignKey("dbo.PlannerItems", "RecipeId", "dbo.Recipes");
            DropIndex("dbo.PlannerItems", new[] { "Planner_PlannerId" });
            DropIndex("dbo.PlannerItems", new[] { "RecipeId" });
            DropTable("dbo.Planners");
            DropTable("dbo.PlannerItems");
        }
    }
}
