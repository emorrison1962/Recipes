use Recipes

--truncate table [dbo].[RecipeTag]
delete from [dbo].[ProcedureItems]
delete from [dbo].[IngredientItems]
delete from [dbo].[IngredientGroups]
delete from [dbo].[ProcedureGroups]

delete from [Recipes].[dbo].[Recipes]


delete from [dbo].[PlannerItems]
delete from [dbo].[PlannerGroups]
delete from [dbo].[Planners]

