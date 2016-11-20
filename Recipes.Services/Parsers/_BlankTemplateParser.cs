using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Recipes.Services.Parsers
{
	class BlankTemplateParser : PageParserBase
	{
		protected override void GetIngredients()
		{
			throw new NotImplementedException();
			var node = base.GetNode("nodeType", "className");
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
				var ingredient = node.InnerText.Trim();
				this.Ingredients.Add(ingredient);
			}
		}

		protected override void GetProcedures()
		{
			throw new NotImplementedException();
			var node = base.GetNode("nodeType", "className");
			if (null != node)
			{
				this.GetDirections(node);
			}
		}

		private void GetDirections(HtmlNode parent)
		{
			var nodes = parent.Descendants(LI);
			foreach (var node in nodes)
			{
				var procedure = node.InnerText.Trim();
				this.Procedures.Add(procedure);
			}
		}
	}
}
