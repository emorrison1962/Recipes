using HtmlAgilityPack;
using Recipes.Domain;
using System;

namespace Recipes.Services
{
	internal class AltonBrownParser : PageParserBase
	{
#pragma warning disable 162
        protected override void GetIngredients()
		{
            var div = base.GetNode(OL, "blog-yumprint-ingredients");
			if (null != div)
			{
				this.GetIngredients(div);
			}
		}
#pragma warning restore 162

        void GetIngredients(HtmlNode div)
		{
			var lis = div.Descendants(LI);
			foreach (var li in lis)
			{
				var ingredient = li.InnerText.FromHtml();
				this.AddIngredient(ingredient);
			}
		}

#pragma warning disable 162
        protected override void GetProcedures()
		{
            var div = base.GetNode(OL, "blog-yumprint-methods");
			if (null != div)
			{
				this.GetDirections(div);
			}
		}
#pragma warning restore 162

        private void GetDirections(HtmlNode div)
		{
			var lis = div.Descendants(LI);
			foreach (var li in lis)
			{
				var procedure = li.InnerText.FromHtml();
				this.Add(new ProcedureGroupItem(procedure));
			}
		}
	}
}