using HtmlAgilityPack;
using Recipes.Domain;
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
					var ingredient = node.InnerText.FromHtml();
					ingredient = ingredient.Replace("  ", " ").Trim();
					this.AddIngredient(ingredient);
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
					var procedure = li.InnerText.FromHtml();
					this.Add(new ProcedureItem(procedure));
				}
			}

			Debug.Assert(this.ProcedureGroups.Count > 0);
		}
	}
}
