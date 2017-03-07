using HtmlAgilityPack;
using Recipes.Domain;
using System;
using System.Linq;

namespace Recipes.Services
{
	internal class DavidLebowitzParser : PageParserBase
	{
		protected override void GetIngredients()
		{
			var div = base.GetNode(DIV, "wpurp-recipe-ingredient");
			if (null != div)
			{
				this.GetIngredients(div);
			}
		}

		void GetIngredients(HtmlNode div)
		{
			var spans = div.Descendants(SPAN).ToList();
			var count = spans.Count();
			if (count % 3 != 0)
				throw new Exception("Unexpected Html Schema.");
			 
			for (int i = 0; i < count; i+= 3)
			{
				var span1 = spans[i];
				var span2 = spans[i+1];
				var span3 = spans[i+2];

				var s1 = span1.InnerText.FromHtml().Trim();
				var s2 = span2.InnerText.FromHtml().Trim();
				var s3 = span3.InnerText.FromHtml().Trim();



				var ingredient = string.Format("{0} {1} {2}", s1, s2, s3);
				if (!string.IsNullOrEmpty(ingredient))
					this.Add(ingredient);
			}
		}

		protected override void GetProcedures()
		{
			var div = base.GetNode(DIV, "wpurp-recipe-instruction"); 
			if (null != div)
			{
				this.GetDirections(div);
			}
		}

		private void GetDirections(HtmlNode div)
		{
			var spans = div.Descendants(SPAN);
			foreach (var span in spans)
			{
				var procedure = span.InnerText.FromHtml().Trim();
				if (!string.IsNullOrEmpty(procedure))
					this.Add(new ProcedureGroupItem(procedure));
			}
		}
	}
}