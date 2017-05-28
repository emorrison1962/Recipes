namespace Recipes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _052717_01 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.IngredientItems", name: "IngredientGroup_IngredientGroupId", newName: "IngredientGroupId");
            RenameColumn(table: "dbo.IngredientGroups", name: "Recipe_RecipeId", newName: "RecipeId");
            RenameColumn(table: "dbo.ProcedureGroups", name: "Recipe_RecipeId", newName: "RecipeId");
            RenameColumn(table: "dbo.ProcedureItems", name: "ProcedureGroup_ProcedureGroupId", newName: "ProcedureGroupId");
            RenameColumn(table: "dbo.ShoppingListItems", name: "ShoppingListGroup_ShoppingListGroupId", newName: "ShoppingListGroupId");
            RenameColumn(table: "dbo.ShoppingListGroups", name: "ShoppingList_ShoppingListId", newName: "ShoppingListId");
            RenameIndex(table: "dbo.IngredientGroups", name: "IX_Recipe_RecipeId", newName: "IX_RecipeId");
            RenameIndex(table: "dbo.IngredientItems", name: "IX_IngredientGroup_IngredientGroupId", newName: "IX_IngredientGroupId");
            RenameIndex(table: "dbo.ProcedureGroups", name: "IX_Recipe_RecipeId", newName: "IX_RecipeId");
            RenameIndex(table: "dbo.ProcedureItems", name: "IX_ProcedureGroup_ProcedureGroupId", newName: "IX_ProcedureGroupId");
            RenameIndex(table: "dbo.ShoppingListGroups", name: "IX_ShoppingList_ShoppingListId", newName: "IX_ShoppingListId");
            RenameIndex(table: "dbo.ShoppingListItems", name: "IX_ShoppingListGroup_ShoppingListGroupId", newName: "IX_ShoppingListGroupId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ShoppingListItems", name: "IX_ShoppingListGroupId", newName: "IX_ShoppingListGroup_ShoppingListGroupId");
            RenameIndex(table: "dbo.ShoppingListGroups", name: "IX_ShoppingListId", newName: "IX_ShoppingList_ShoppingListId");
            RenameIndex(table: "dbo.ProcedureItems", name: "IX_ProcedureGroupId", newName: "IX_ProcedureGroup_ProcedureGroupId");
            RenameIndex(table: "dbo.ProcedureGroups", name: "IX_RecipeId", newName: "IX_Recipe_RecipeId");
            RenameIndex(table: "dbo.IngredientItems", name: "IX_IngredientGroupId", newName: "IX_IngredientGroup_IngredientGroupId");
            RenameIndex(table: "dbo.IngredientGroups", name: "IX_RecipeId", newName: "IX_Recipe_RecipeId");
            RenameColumn(table: "dbo.ShoppingListGroups", name: "ShoppingListId", newName: "ShoppingList_ShoppingListId");
            RenameColumn(table: "dbo.ShoppingListItems", name: "ShoppingListGroupId", newName: "ShoppingListGroup_ShoppingListGroupId");
            RenameColumn(table: "dbo.ProcedureItems", name: "ProcedureGroupId", newName: "ProcedureGroup_ProcedureGroupId");
            RenameColumn(table: "dbo.ProcedureGroups", name: "RecipeId", newName: "Recipe_RecipeId");
            RenameColumn(table: "dbo.IngredientGroups", name: "RecipeId", newName: "Recipe_RecipeId");
            RenameColumn(table: "dbo.IngredientItems", name: "IngredientGroupId", newName: "IngredientGroup_IngredientGroupId");
        }
    }
}
