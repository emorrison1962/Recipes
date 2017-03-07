using HtmlAgilityPack;
using Recipes.Domain;

namespace Recipes.Services
{
	internal class TwiceCookedHalfBakedParser : PageParserBase
	{
		protected override void GetIngredients()
		{
		}

		void GetIngredients(HtmlNode div)
		{
		}

		protected override void GetProcedures()
		{
			var div = base.GetNode(DIV, "entry-content");
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
				var procedure = p.InnerText.FromHtml();
				this.Add(new ProcedureGroupItem(procedure));
			}
		}
	}
}