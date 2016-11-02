using HtmlAgilityPack;
using Recipes.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Services.Parsers
{
	public class SeriousEatsParser : PageParserBase
	{

		override protected bool GetIngredients()
		{
			var result = false;
			var div = GetIngredientsDiv();
			if (null != div)
			{
				var list = this.GetIngredientsList(div);
				if (null != list)
				{
					this.Ingredients = this.GetIngredients(list);
					if (this.Ingredients.Count > 0)
						result = true;
				}
			}

			return result;
		}

		HtmlNode GetIngredientsDiv()
		{
			var divs = this.HtmlDocument.DocumentNode.Descendants(DIV);
			var result = divs.ByClass("ingredients-warpper").FirstOrDefault();

			return result;
		}

		HtmlNode GetIngredientsList(HtmlNode div)
		{
			HtmlNode result = null;
			result = div.Descendants().ByClass("cs-ingredient").FirstOrDefault();

			return result;
		}

		List<string> GetIngredients(HtmlNode list)
		{
			var result = new List<string>();

			if (null != list)
			{
				foreach (var li in list.ChildNodes)
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
				var list = this.GetProceduresList(div);
				if (null != list)
				{
					this.Procedures = this.GetProcedures(list);
					if (this.Procedures.Count > 0)
						result = true;
				}
			}

			return result;
		}

		HtmlNode GetProceduresDiv()
		{
			var divs = this.HtmlDocument.DocumentNode.Descendants(DIV);
			var result = divs.Where(x => x.HasAttributes
					&& x.Attributes.Where(a => CLASS == a.Name && "recipe-procedures" == a.Value).Count() > 0
					).FirstOrDefault();

			return result;
		}

		HtmlNode GetProceduresList(HtmlNode div)
		{
			HtmlNode result = null;

			result = div.Descendants(UL).FirstOrDefault();
			if (null == result)
				result = div.Descendants(OL).FirstOrDefault();

			return result;
		}

		List<string> GetProcedures(HtmlNode list)
		{
			var result = new List<string>();

			if (null != list)
			{
				//result = list.Descendants("li").Select(x => x.InnerText).ToList();

				foreach (var li in list.ChildNodes)
				{
					if (li.NodeType == HtmlNodeType.Element)
					{
						result.Add(li.InnerText);
					}
				}
			}

			return result;
		}

	}//class
}
