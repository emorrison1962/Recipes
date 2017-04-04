namespace Recipes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _040317_05 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlannerItems", "Planner_PlannerId", "dbo.Planners");
            DropIndex("dbo.PlannerItems", new[] { "Planner_PlannerId" });
            CreateTable(
                "dbo.PlannerGroups",
                c => new
                    {
                        PlannerGroupId = c.Int(nullable: false, identity: true),
                        Weekday = c.Int(nullable: false),
                        Text = c.String(),
                        Planner_PlannerId = c.Int(),
                    })
                .PrimaryKey(t => t.PlannerGroupId)
                .ForeignKey("dbo.Planners", t => t.Planner_PlannerId)
                .Index(t => t.Planner_PlannerId);
            
            AddColumn("dbo.PlannerItems", "Text", c => c.String());
            AddColumn("dbo.PlannerItems", "PlannerGroup_PlannerGroupId", c => c.Int());
            CreateIndex("dbo.PlannerItems", "PlannerGroup_PlannerGroupId");
            AddForeignKey("dbo.PlannerItems", "PlannerGroup_PlannerGroupId", "dbo.PlannerGroups", "PlannerGroupId");
            DropColumn("dbo.PlannerItems", "Weekday");
            DropColumn("dbo.PlannerItems", "Planner_PlannerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlannerItems", "Planner_PlannerId", c => c.Int());
            AddColumn("dbo.PlannerItems", "Weekday", c => c.Int(nullable: false));
            DropForeignKey("dbo.PlannerGroups", "Planner_PlannerId", "dbo.Planners");
            DropForeignKey("dbo.PlannerItems", "PlannerGroup_PlannerGroupId", "dbo.PlannerGroups");
            DropIndex("dbo.PlannerItems", new[] { "PlannerGroup_PlannerGroupId" });
            DropIndex("dbo.PlannerGroups", new[] { "Planner_PlannerId" });
            DropColumn("dbo.PlannerItems", "PlannerGroup_PlannerGroupId");
            DropColumn("dbo.PlannerItems", "Text");
            DropTable("dbo.PlannerGroups");
            CreateIndex("dbo.PlannerItems", "Planner_PlannerId");
            AddForeignKey("dbo.PlannerItems", "Planner_PlannerId", "dbo.Planners", "PlannerId");
        }
    }
}
