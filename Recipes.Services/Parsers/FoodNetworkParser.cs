using HtmlAgilityPack;
using System.Collections.Generic;

namespace Recipes.Services.Parsers
{
	class FoodNetworkParser : PageParserBase
	{
		protected override bool GetIngredients(HtmlDocument doc)
		{
			var result = false;
			var div = base.GetIngredientsDiv(doc, "col8 ingredients responsive");
			if (null != div)
			{
				this.GetIngredients(div);
				if (this.Ingredients.Count > 0)
					result = true;
			}

			return result;
		}

		IEnumerable<HtmlNode> GetIngredients(HtmlNode div)
		{
			var result = new List<HtmlNode>();
			var lis = div.Descendants(LI);
			foreach (var li in lis)
			{
				var ingredient = li.InnerText;
				this.Ingredients.Add(ingredient);
			}
			return result;
		}

		protected override bool GetProcedures(HtmlDocument doc)
		{
			var result = false;
#warning This is not returning the div. Need to figure out the " responsive" addition to the css class.
			var div = base.GetProceduresDiv(doc, "col10 directions responsive");

			if (null != div)
			{
				this.GetDirections(div);
				if (this.Procedures.Count > 0)
					result = true;
			}

			return result;
		}

		private void GetDirections(HtmlNode div)
		{
			var lis = div.Descendants(LI);
			foreach (var li in lis)
			{
				var ps = li.Descendants("p");
				foreach (var p in ps)
				{
					var procedure = p.InnerText;
					this.Procedures.Add(procedure);
				}
			}
		}
	}
}
