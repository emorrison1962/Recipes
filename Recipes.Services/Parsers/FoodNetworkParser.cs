using HtmlAgilityPack;
using System.Collections.Generic;

namespace Recipes.Services.Parsers
{
	class FoodNetworkParser : PageParserBase
	{
		protected override bool GetIngredients()
		{
			var result = false;
			var div = base.GetIngredientsDiv("col8 ingredients");
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

		protected override bool GetProcedures()
		{
			var result = false;
			var div = base.GetProceduresDiv("col10 directions");

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
