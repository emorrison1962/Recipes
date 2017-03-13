namespace Recipes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _03121703 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.IngredientGroupItems", name: "IngredientGroup_IngredientGroupId", newName: "IngredientGroupRefId");
            RenameIndex(table: "dbo.IngredientGroupItems", name: "IX_IngredientGroup_IngredientGroupId", newName: "IX_IngredientGroupRefId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.IngredientGroupItems", name: "IX_IngredientGroupRefId", newName: "IX_IngredientGroup_IngredientGroupId");
            RenameColumn(table: "dbo.IngredientGroupItems", name: "IngredientGroupRefId", newName: "IngredientGroup_IngredientGroupId");
        }
    }
}
