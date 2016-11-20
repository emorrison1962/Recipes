using HtmlAgilityPack;
using System;

namespace Recipes.Services
{
	internal class AltonBrownParser : PageParserBase
	{
#pragma warning disable 162
        protected override void GetIngredients()
		{
            throw new NotImplementedException();
            var div = base.GetNode(DIV, "ingredients_classname");
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
				var ingredient = li.InnerText.Trim();
				this.Ingredients.Add(ingredient);
			}
		}

#pragma warning disable 162
        protected override void GetProcedures()
		{
            throw new NotImplementedException();
            var div = base.GetNode(DIV, "directions_classname");
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
				var procedure = li.InnerText.Trim();
				this.Procedures.Add(procedure);
			}
		}
	}
}