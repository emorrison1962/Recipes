namespace Recipes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _03121705 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ProcedureGroupItems", name: "ProcedureGroup_ProcedureGroupId", newName: "ProcedureGroupRefId");
            RenameIndex(table: "dbo.ProcedureGroupItems", name: "IX_ProcedureGroup_ProcedureGroupId", newName: "IX_ProcedureGroupRefId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ProcedureGroupItems", name: "IX_ProcedureGroupRefId", newName: "IX_ProcedureGroup_ProcedureGroupId");
            RenameColumn(table: "dbo.ProcedureGroupItems", name: "ProcedureGroupRefId", newName: "ProcedureGroup_ProcedureGroupId");
        }
    }
}
