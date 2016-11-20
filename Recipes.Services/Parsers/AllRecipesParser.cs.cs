using HtmlAgilityPack;

namespace Recipes.Services.Parsers
{
	class AllRecipesParser : PageParserBase
	{
		protected override void GetIngredients()
		{
			var div = base.GetNode(DIV, "recipe-detail");
			if (null != div)
			{
				this.GetIngredients(div);
			}
		}

		void GetIngredients(HtmlNode div)
		{
			var lis = div.Descendants(LI);
			foreach (var li in lis)
			{
				var ingredient = li.InnerText.Trim();
				this.Ingredients.Add(ingredient);
			}
		}

		protected override void GetProcedures()
		{
			var div = base.GetNode(DIV, "directions");
			if (null != div)
			{
				this.GetDirections(div);
			}
		}

		private void GetDirections(HtmlNode div)
		{
			var lis = div.Descendants(LI);
			foreach (var li in lis)
			{
				var procedure = li.InnerText.Trim();
				this.Procedures.Add(procedure);
			}
		}
	}
}
