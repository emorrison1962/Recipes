using HtmlAgilityPack;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Recipes.Services.Parsers
{
	class FoodDotComParser : PageParserBase
	{
		protected override HtmlNode GetIngredientsDiv(string className)
		{
			var sections = this.HtmlDocument.DocumentNode.Descendants(SECTION);
			var section = sections.ByClass("ingredients-instructions").First();
			var result = section.Descendants(DIV).ByClass("ingredients").First();

			Debug.Assert(null != result);
			return result;
		}
		protected override bool GetIngredients()
		{
			var result = false;
			var div = base.GetIngredientsDiv("recipe-detail");
			if (null != div)
			{
				this.GetIngredients(div);
				if (this.Ingredients.Count > 0)
					result = true;
			}

			Debug.Assert(result);
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

			Debug.Assert(null != result);
			return result;
		}

		protected override bool GetProcedures()
		{
			var result = false;
			var div = base.GetProceduresDiv("directions");

			if (null != div)
			{
				this.GetDirections(div);
				if (this.Procedures.Count > 0)
					result = true;
			}

			Debug.Assert(result);
			return result;
		}

		private void GetDirections(HtmlNode div)
		{
			var lis = div.Descendants(LI);
			foreach (var li in lis)
			{
				var procedure = li.InnerText;
				this.Procedures.Add(procedure);
			}
		}
	}
}
