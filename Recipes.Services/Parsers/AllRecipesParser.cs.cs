using HtmlAgilityPack;
using Recipes.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Recipes.Services.Parsers
{
	class AllRecipesParser : PageParserBase
	{
		protected override void GetIngredients()
		{
			var containers = new List<HtmlNode>();
			var c1 = base.GetNode(UL, "list-ingredients-1");
			var c2 = base.GetNode(UL, "list-ingredients-2");
			containers.Add(c1);
			containers.Add(c2);
			
			if (null != containers)
			{
				this.GetIngredients(containers);
			}
		}

		void GetIngredients(List<HtmlNode> containers)
		{
			foreach (var container in containers)
			{
				var spans = base.GetNodes(container, SPAN, "recipe-ingred_txt");

				foreach (var span in spans)
				{
					var ingredient = span.InnerText.FromHtml().Trim();
					if (!string.IsNullOrEmpty(ingredient))
						this.Add(new IngredientGroupItem(ingredient));
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
				var span = base.GetNode(li, SPAN, "recipe-directions__list--item");
				if (null != span)
				{
					var procedure = span.InnerText.FromHtml().Trim();
					if (!string.IsNullOrEmpty(procedure))
						this.Add(new ProcedureGroupItem(procedure));
				}
			}
		}
	}
}
