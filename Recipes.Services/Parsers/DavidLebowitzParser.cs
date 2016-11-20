using HtmlAgilityPack;
using System.Linq;

namespace Recipes.Services
{
	internal class DavidLebowitzParser : PageParserBase
	{
		protected override void GetIngredients()
		{
			var div = this.HtmlDocument.DocumentNode.Descendants("blockquote").First();
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
			var div = this.HtmlDocument.DocumentNode.Descendants("blockquote").First();
			if (null != div)
			{
				this.GetDirections(div);
			}
		}

		private void GetDirections(HtmlNode div)
		{
			var ps = div.Descendants(P);
			foreach (var p in ps)
			{
				var procedure = p.InnerText.Trim();
				this.Procedures.Add(procedure);
			}
		}
	}
}