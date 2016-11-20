using HtmlAgilityPack;

namespace Recipes.Services
{
	internal class ElectroluxParser : PageParserBase
	{
		protected override void GetIngredients()
		{
			var div = base.GetNode(DIV, "recipe-ingredients");
			if (null != div)
			{
				this.GetIngredients(div);
			}
		}

		void GetIngredients(HtmlNode parent)
		{
			var nodes = parent.Descendants(P);
			foreach (var node in nodes)
			{
				var ingredient = node.InnerText.Trim();
				this.Ingredients.Add(ingredient);
			}
		}

		protected override void GetProcedures()
		{
			var div = base.GetNode(DIV, "recipe-instructions");
			if (null != div)
			{
				this.GetDirections(div);
			}
		}

		private void GetDirections(HtmlNode parent)
		{
			var nodes = parent.Descendants(P);
			foreach (var node in nodes)
			{
				var procedure = node.InnerText.Trim();
				this.Procedures.Add(procedure);
			}
		}
	}
}