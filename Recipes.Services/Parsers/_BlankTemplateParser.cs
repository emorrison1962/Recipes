using HtmlAgilityPack;
using Recipes.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Recipes.Services.Parsers
{
	class BlankTemplateParser : PageParserBase
	{
#pragma warning disable 162
        protected override void GetIngredients()
		{
            throw new NotImplementedException();
            var node = base.GetNode("nodeType", "className");
			if (null != node)
			{
				this.GetIngredients(node);
			}
		}
#pragma warning restore 162

        void GetIngredients(HtmlNode parent)
		{
			var nodes = parent.Descendants(LI);
			foreach (var node in nodes)
			{
				var ingredient = node.InnerText.FromHtml();
				this.AddIngredient(ingredient);
			}
		}

#pragma warning disable 162
        protected override void GetProcedures()
		{
            throw new NotImplementedException();
            var node = base.GetNode("nodeType", "className");
			if (null != node)
			{
				this.GetDirections(node);
			}
		}
#pragma warning restore 162

        private void GetDirections(HtmlNode parent)
		{
			var nodes = parent.Descendants(LI);
			foreach (var node in nodes)
			{
				var procedure = node.InnerText.FromHtml();
				this.Add(new ProcedureGroupItem(procedure));
			}
		}
	}
}
