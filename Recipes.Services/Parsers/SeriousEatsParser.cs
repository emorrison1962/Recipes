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
            var node = this.GetIngredientsContainerNode();
            if (null != node)
            {
                var ingredients = this.GetIngredientGroups(node);
                ingredients.ForEach(x => this.Add(x));
            }
        }

        HtmlNode GetIngredientsContainerNode()
        {
            HtmlNode result = null;
            var div = GetIngredientsDiv();
            if (null != div)
            {
                result = div.Descendants(UL).FirstOrDefault();
            }

            return result;
        }

        HtmlNode GetIngredientsDiv()
        {
            var divs = this.HtmlDocument.DocumentNode.Descendants(DIV);
            var result = divs.ByClass("recipe-ingredients").FirstOrDefault();

            return result;
        }

        List<IngredientGroup> GetIngredientGroups(HtmlNode parentNode)
        {
            var result = new List<IngredientGroup>();

            if (null != parentNode)
            {
                foreach (var li in parentNode.ChildNodes)
                {
                    if (li.NodeType == HtmlNodeType.Element)
                    {
                        var strong = li.GetNode("strong");
                        if (null != strong)
                        {
                            result.Add(new IngredientGroup(strong.InnerText.FromHtml()));
                        }
                        else
                        {
                            var group = result.LastOrDefault();
                            if (null == group)
                            {
                                group = new IngredientGroup();
                                result.Add(group);
                            }
                            group.Add(new IngredientItem(li.InnerText.FromHtml()));
                        }
                    }
                }
            }

            return result;
        }

        override protected void GetProcedures()
        {
            var node = this.GetProceduresContainerNode();
            if (null != node)
            {
                var procedures = this.GetProcedureGroups(node);
                procedures.ForEach(x => this.Add(x));
            }
        }

        HtmlNode GetProceduresContainerNode()
        {
            HtmlNode result = null;

            var div = GetProceduresDiv();
            if (null != div)
            {

                result = div.Descendants(UL).FirstOrDefault();
                if (null == result)
                    result = div.Descendants(OL).FirstOrDefault();
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

        List<ProcedureGroup> GetProcedureGroups(HtmlNode parentNode)
        {
            var result = new List<ProcedureGroup>();

            if (null != parentNode)
            {
                foreach (var li in parentNode.ChildNodes)
                {
                    if (li.NodeType == HtmlNodeType.Element)
                    {
                        var strong = li.GetNode("strong");
                        var strongText = string.Empty;
                        if (null != strong)
                        {
                            strongText = strong.InnerText.FromHtml();
                            result.Add(new ProcedureGroup(strongText));
                        }

                        var divs = li.Descendants(DIV);
                        var sb = new StringBuilder();
                        foreach (var div in divs)
                        {
                            var s = div.InnerText.FromHtml().Trim();
                            if (!string.IsNullOrEmpty(s))
                                sb.AppendFormat("{0} ", s);
                        }

                        var ingredientText = sb.ToString().Trim();
                        if (!string.IsNullOrEmpty(strongText))
                        {
                            ingredientText = ingredientText.Replace(strongText, string.Empty);
                            ingredientText = ingredientText.Replace("  ", " ").Trim();
                        }

                        if (!string.IsNullOrEmpty(ingredientText))
                        {
                            var group = result.LastOrDefault();
                            if (null == group)
                            {
                                group = new ProcedureGroup();
                                result.Add(group);
                            }
                            group.Add(new ProcedureItem(ingredientText));
                        }
                    }
                }
            }

            return result;
        }

    }//class
}
