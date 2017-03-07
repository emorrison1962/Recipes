using HtmlAgilityPack;
using Recipes.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Recipes.Services.Parsers
{
	public class EpicurousParser : PageParserBase
	{
		override protected void GetIngredients()
		{
			var div = GetIngredientsDiv();
			if (null != div)
			{
				var ingredientGroups = this.GetIngredientGroups(div);
				if (null != ingredientGroups)
				{
					foreach (var ingredientGroup in ingredientGroups)
					{
						this.Add(new IngredientGroup());
						var ingredients = this.GetIngredients(ingredientGroup);
						ingredients.ForEach(x => this.Add(x));
					}
				}
			}
		}

		HtmlNode GetIngredientsDiv()
		{
			const string INGREDIENTS = "ingredients-info";
			var result = this.HtmlDocument.DocumentNode.Descendants(DIV).ByClass(INGREDIENTS).FirstOrDefault();

			return result;
		}

		List<HtmlNode> GetIngredientGroups(HtmlNode div)
		{
			List<HtmlNode> result = null;

			var lis = div.Descendants(LI);
			result = lis.ByClass("ingredient-group").ToList();

			return result;
		}

		List<string> GetIngredients(HtmlNode ingredientGroup)
		{
			var result = new List<string>();

			if (null != ingredientGroup)
			{
				var strong = ingredientGroup.Descendants("strong").First();
				result.Add(strong.InnerText.FromHtml());

				foreach (var li in ingredientGroup.Descendants(LI))
				{
					if (li.NodeType == HtmlNodeType.Element)
					{
						result.Add(li.InnerText.FromHtml());
					}
				}
			}

			return result;
		}


		override protected void GetProcedures()
		{
			var div = GetProceduresDiv();
			if (null != div)
			{
				var preparationGroups = this.GetPreparationGroups(div);
				if (null != preparationGroups)
				{
					foreach (var preparationGroup in preparationGroups)
					{
						this.Add(new ProcedureGroup());
						var preparation = this.GetProcedures(preparationGroup);
						preparation.ForEach(x => this.Add(new ProcedureGroupItem(x)));
					}
				}
			}
		}

		HtmlNode GetProceduresDiv()
		{
			var divs = this.HtmlDocument.DocumentNode.Descendants(DIV);
			var result = divs.ByClass("instructions").FirstOrDefault();

			return result;
		}

		List<HtmlNode> GetPreparationGroups(HtmlNode div)
		{
			var result = div.Descendants(LI).ByClass("preparation-group").ToList();
			return result;
		}

		List<string> GetProcedures(HtmlNode preparationGroup)
		{
			var result = new List<string>();

			if (null != preparationGroup)
			{
				var strong = preparationGroup.Descendants("strong").First();
				result.Add(strong.InnerText.FromHtml());

				foreach (var li in preparationGroup.Descendants(LI))
				{
					if (li.NodeType == HtmlNodeType.Element)
					{
						result.Add(li.InnerText.FromHtml());
					}
				}
			}

			return result;
		}

	}//class
}
