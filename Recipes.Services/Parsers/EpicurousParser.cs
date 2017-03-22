using HtmlAgilityPack;
using Recipes.Domain;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Recipes.Services.Parsers
{
	public class EpicurousParser : PageParserBase
	{
		override protected void GetIngredients()
		{
			var div = GetIngredientsContainerNode();
			if (null != div)
			{
				var igNodes = this.GetIngredientGroupNodes(div);
				if (null != igNodes)
				{
					foreach (var igNode in igNodes)
					{
						var group = this.GetIngredientGroup(igNode);
						this.Add(group);
					}
				}
			}
		}

		HtmlNode GetIngredientsContainerNode()
		{
			const string INGREDIENTS = "ingredients-info";
			var result = this.HtmlDocument.DocumentNode.Descendants(DIV).ByClass(INGREDIENTS).FirstOrDefault();

			return result;
		}

		List<HtmlNode> GetIngredientGroupNodes(HtmlNode div)
		{
			List<HtmlNode> result = null;

			var lis = div.Descendants(LI);
			result = lis.ByClass("ingredient-group").ToList();

			return result;
		}

		IngredientGroup GetIngredientGroup(HtmlNode igNode)
		{
            IngredientGroup result = null;

            Debug.WriteLine(igNode.InnerHtml);

			if (null != igNode)
			{
                var strong = igNode.Descendants("strong").First();
                var groupText = strong.InnerText.FromHtml();
                result = new IngredientGroup(groupText);

				foreach (var li in igNode.Descendants(LI))
				{
					if (li.NodeType == HtmlNodeType.Element)
					{
                        var itemText = li.InnerText.FromHtml();
                        result.Add(new IngredientItem(itemText));
					}
				}
			}

			return result;
		}


		override protected void GetProcedures()
		{
			var div = GetProceduresContainerNode();
			if (null != div)
			{
				var pgNodes = this.GetPreparationGroupNodes(div);
				if (null != pgNodes)
				{
					foreach (var pgNode in pgNodes)
					{
						var group = this.GetProcedureGroup(pgNode);
                        this.Add(group);
					}
				}
			}
		}

		HtmlNode GetProceduresContainerNode()
		{
			var divs = this.HtmlDocument.DocumentNode.Descendants(DIV);
			var result = divs.ByClass("instructions").FirstOrDefault();

			return result;
		}

		List<HtmlNode> GetPreparationGroupNodes(HtmlNode div)
		{
			var result = div.Descendants(LI).ByClass("preparation-group").ToList();
			return result;
		}

        ProcedureGroup GetProcedureGroup(HtmlNode pgNode)
		{
            ProcedureGroup result = null;

			if (null != pgNode)
			{
				var strong = pgNode.Descendants("strong").First();
                var groupText = strong.InnerText.FromHtml();
                result = new ProcedureGroup(groupText);

				foreach (var li in pgNode.Descendants(LI))
				{
					if (li.NodeType == HtmlNodeType.Element)
					{
                        var itemText = li.InnerText.FromHtml();
                        result.Add(new ProcedureItem(itemText));
					}
				}
			}

			return result;
		}

	}//class
}
