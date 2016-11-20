using HtmlAgilityPack;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace Recipes.Services.Parsers
{
	class FoodDotComParser : PageParserBase
	{
		protected HtmlNode GetIngredientsNode(string className)
		{
			var sections = this.HtmlDocument.DocumentNode.Descendants(SECTION);
			var section = sections.ByClass("ingredients-instructions").First();
			var result = section.Descendants(DIV).ByClass("ingredients").First();

			Debug.Assert(null != result);
			return result;
		}
		protected override void GetIngredients()
		{
			var node = base.GetNode(DIV, "recipe-detail");
			if (null != node)
			{
				this.GetIngredients(node);
			}
		}

		void GetIngredients(HtmlNode parent)
		{
			var nodes = parent.Descendants(LI);
			foreach (var node in nodes)
			{
				var divs = node.Descendants(DIV);
				if (0 == divs.Count())
				{
					var ingredient = node.InnerText;
					ingredient = WebUtility.HtmlDecode(ingredient).Replace("  ", " ").Trim();
					this.Ingredients.Add(ingredient);
				}
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
				var divs = li.Descendants(DIV);
				if (0 == divs.Count())
				{
					var procedure = li.InnerText.Trim();
					this.Procedures.Add(procedure);
				}
			}

			Debug.Assert(this.Procedures.Count > 0);
		}
	}
}
