select distinct * from 
[dbo].[Recipes] r 
	--left join [dbo].[RecipeTag] t
	--	on r.RecipeId = t.RecipeRefId

	--join [dbo].[IngredientGroups] ig
	--	on ig.Recipe_RecipeId = r.RecipeId
	--join [dbo].[IngredientGroupItems] igi
	--	on igi.IngredientGroup_IngredientGroupId = ig.IngredientGroupId

	join [dbo].[ProcedureGroups] pg
		on pg.Recipe_RecipeId = r.RecipeId
	join [dbo].[ProcedureGroupItems] pgi
		on pg.ProcedureGroupId = pgi.ProcedureGroup_ProcedureGroupId























