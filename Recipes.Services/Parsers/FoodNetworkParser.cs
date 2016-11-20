using HtmlAgilityPack;
using System.Diagnostics;
using System.Linq;

namespace Recipes.Services.Parsers
{
	class FoodNetworkParser : PageParserBase
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
			var div = base.GetNode(DIV, "col8 ingredients");
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
			var div = base.GetNode(DIV, "col10 directions");
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
				var ps = li.Descendants("p");
				foreach (var p in ps)
				{
					var procedure = p.InnerText.Trim();
					this.Procedures.Add(procedure);
				}
			}
		}
	}
}
