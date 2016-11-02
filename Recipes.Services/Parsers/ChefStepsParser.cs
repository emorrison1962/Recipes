using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Services.Parsers
{
	public class ChefStepsParser : PageParserBase
	{

		public ChefStepsParser()
		{

		}

		override protected bool GetIngredients()
		{
			var result = false;

			//<div class="recipe-ingredients">

			var div = GetIngredientsDiv();
			if (null != div)
			{
				var ingredientGroups = this.GetIngredientGroups(div);
				if (null != ingredientGroups)
				{
					foreach (var ingredientGroup in ingredientGroups)
					{
						var ingredients = this.GetIngredients(ingredientGroup);
						this.Ingredients.AddRange(ingredients);
					}
					if (this.Ingredients.Count > 0)
						result = true;
				}
			}

			return result;
		}

		HtmlNode GetIngredientsDiv()
		{
			var result = base.GetIngredientsDiv("ingredients-wrapper");
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
				result.Add(strong.InnerText);

				foreach (var li in ingredientGroup.Descendants(LI))
				{
					if (li.NodeType == HtmlNodeType.Element)
					{
						result.Add(li.InnerText);
					}
				}
			}

			return result;
		}


		override protected bool GetProcedures()
		{
			var result = false;

			var div = GetProceduresDiv();
			if (null != div)
			{
				var preparationGroups = this.GetPreparationGroups(div);
				if (null != preparationGroups)
				{
					foreach (var preparationGroup in preparationGroups)
					{
						var preparation = this.GetProcedures(preparationGroup);
						this.Procedures.AddRange(preparation);
					}
					if (this.Procedures.Count > 0)
						result = true;
				}
			}

			return result;
		}

		HtmlNode GetProceduresDiv()
		{
			var result = base.GetProceduresDiv("instructions");
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
				result.Add(strong.InnerText);

				foreach (var li in preparationGroup.Descendants(LI))
				{
					if (li.NodeType == HtmlNodeType.Element)
					{
						result.Add(li.InnerText.Trim());
					}
				}
			}

			return result;
		}


	}//class
}//ns
