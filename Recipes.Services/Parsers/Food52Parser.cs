using HtmlAgilityPack;
using Recipes.Domain;
using System.Diagnostics;
using System.Linq;

namespace Recipes.Services
{
	internal class Food52Parser : PageParserBase
	{
		protected override void GetIngredients()
		{
			var node = base.GetNode(UL, "recipe-list");
			if (null != node)
			{
				this.GetIngredients(node);
			}
		}

		void GetIngredients(HtmlNode parent)
		{
#if false
			<li itemprop="ingredients">
				<span class="recipe-list-quantity">3</span>
				<span class="recipe-list-item-name">
					cups all-purpose flour, divided
				</span>
			</li>
#endif
			var nodes = parent.Descendants(LI);
			foreach (var node in nodes)
			{
				var spans = node.Descendants(SPAN).ToArray();
				Debug.Assert(2 == spans.Count());
				if (2 != spans.Count())
					throw new PageParsingException("Unexpectedhtml html schema");
				var span0Txt = spans[0].InnerText.FromHtml();
				var span1Txt = spans[1].InnerText.FromHtml();
				var ingredient = string.Format("{0} {1}", span0Txt, span1Txt);
				this.Add(ingredient);
			}
		}

		protected override void GetProcedures()
		{
			var node = base.GetNode(UL, "recipe-list").ParentNode.Descendants(OL).First();

			if (null != node)
			{
				this.GetDirections(node);
			}
		}

		private void GetDirections(HtmlNode parent)
		{
			var lis = parent.Descendants(LI);
			foreach (var li in lis)
			{
				var procedure = li.InnerText.FromHtml();
				this.Add(new ProcedureGroupItem(procedure));
			}
		}
	}
}