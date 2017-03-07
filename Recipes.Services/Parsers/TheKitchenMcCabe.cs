using HtmlAgilityPack;
using Recipes.Domain;

namespace Recipes.Services.Parsers
{
	class TheKitchenMcCabeParser : PageParserBase
	{
		protected override void GetIngredients()
		{
			var div = base.GetNode(DIV, "ERSIngredients");
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
				var ingredient = li.InnerText.FromHtml();
				this.Add(ingredient);
			}
		}

		protected override void GetProcedures()
		{
			var div = base.GetNode(DIV, "ERSInstructions");
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
				var procedure = li.InnerText.FromHtml();
				this.Add(new ProcedureGroupItem(procedure));
			}
		}
	}
}
