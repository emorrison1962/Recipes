using HtmlAgilityPack;
using System;

namespace Recipes.Services
{
	internal class AltonBrownParser : PageParserBase
	{
		protected override void GetIngredients()
		{
			throw new NotImplementedException();
			var div = base.GetNode(DIV, "ingredients_classname");
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
			throw new NotImplementedException();
			var div = base.GetNode(DIV, "directions_classname");
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
				var procedure = li.InnerText.Trim();
				this.Procedures.Add(procedure);
			}
		}
	}
}