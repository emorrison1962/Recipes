using HtmlAgilityPack;
using Recipes.Domain;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Recipes.Services.Parsers
{
	public class SeriousEatsParser : PageParserBase
	{

		override protected void GetIngredients()
		{
			var div = GetIngredientsDiv();
			if (null != div)
			{
				var node = this.GetIngredientsList(div);
				if (null != node)
				{
					var ingredients = this.GetIngredients(node);
					ingredients.ForEach(x => this.Add(x));
				}
			}
		}

		HtmlNode GetIngredientsDiv()
		{
			var divs = this.HtmlDocument.DocumentNode.Descendants(DIV);
			var result = divs.ByClass("recipe-ingredients").FirstOrDefault();

			Debug.Assert(null != result);
			return result;
		}

		HtmlNode GetIngredientsList(HtmlNode div)
		{
			HtmlNode result = null;
			result = div.Descendants(UL).FirstOrDefault();
			//result = div.Descendants().ByClass("cs-ingredient").FirstOrDefault();

			Debug.Assert(null != result);
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
						result.Add(li.InnerText.FromHtml());
					}
				}
			}

			Debug.Assert(result.Count > 0);
			return result;
		}


		override protected void GetProcedures()
		{
			var div = GetProceduresDiv();
			if (null != div)
			{
				var node = this.GetProceduresList(div);
				if (null != node)
				{
					var procedures = this.GetProcedures(node);
					procedures.ForEach(x => this.Add(new ProcedureGroupItem(x)));
				}
			}
		}

		HtmlNode GetProceduresDiv()
		{
			var divs = this.HtmlDocument.DocumentNode.Descendants(DIV);
			var result = divs.Where(x => x.HasAttributes
					&& x.Attributes.Where(a => CLASS == a.Name && "recipe-procedures" == a.Value).Count() > 0
					).FirstOrDefault();

			Debug.Assert(null != result);
			return result;
		}

		HtmlNode GetProceduresList(HtmlNode div)
		{
			HtmlNode result = null;

			result = div.Descendants(UL).FirstOrDefault();
			if (null == result)
				result = div.Descendants(OL).FirstOrDefault();

			Debug.Assert(null != result);
			return result;
		}

		List<string> GetProcedures(HtmlNode list)
		{
			var result = new List<string>();

			if (null != list)
			{
                //result = list.Descendants("li").Select(x => x.InnerText.FromHtml()).ToList();

                foreach (var li in list.ChildNodes)
				{
					var divs = li.Descendants(DIV);
					var sb = new StringBuilder();
					foreach (var div in divs)
					{
						var s = div.InnerText.FromHtml().Trim();
						if (!string.IsNullOrEmpty(s))
							sb.AppendFormat("{0} ", s);
					}
					var sbStr = sb.ToString().Trim();
					if (!string.IsNullOrEmpty(sbStr))
						result.Add(sbStr);
				}
			}

			Debug.Assert(result.Count > 0);
			return result;
		}

	}//class
}
