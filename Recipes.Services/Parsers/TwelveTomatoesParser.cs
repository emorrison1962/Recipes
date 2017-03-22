using HtmlAgilityPack;
using Recipes.Domain;
using System.Linq;

namespace Recipes.Services
{
	internal class TwelveTomatoesParser : PageParserBase
	{
		protected override void GetIngredients()
		{
			var div = this.GetNode(DIV, "recipe-ingredients");
			if (null != div)
			{
				this.GetIngredients(div);
			}
		}

		override protected HtmlNode GetNode(string nodeType, string id)
		{
			var nodes = this.HtmlDocument.DocumentNode.Descendants(nodeType);
			var result = nodes.ByID(id).FirstOrDefault();

			return result;
		}


		void GetIngredients(HtmlNode div)
		{
			var lis = div.Descendants(LI);
			foreach (var li in lis)
			{
				var ingredient = li.InnerText.FromHtml();
				this.AddIngredient(ingredient);
			}
		}

		protected override void GetProcedures()
		{
			var div = this.GetNode(DIV, "recipe-prep");
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
				this.Add(new ProcedureItem(procedure));
			}
		}
	}
}