using HtmlAgilityPack;
using System.Linq;
using System.Text;

namespace Recipes.Services
{
    internal class NyTimesParser : PageParserBase
    {
        protected override void GetIngredients()
        {
            var node = base.GetNode("section", "recipe-ingredients-wrap")
                .Descendants(UL).ByClass("recipe-ingredients").First();
            if (null != node)
            {
                this.GetIngredients(node);
            }
        }

        void GetIngredients(HtmlNode parent)
        {
            var lis = parent.Descendants(LI);
#if false
				<li itemprop="recipeIngredient">
					<span class="quantity">1</span>
					<span class="ingredient-name">teaspoon <span>kosher salt</span></span>
				</li>
#endif
            foreach (var li in lis)
            {
                var sb = new StringBuilder();
                var spans = li.Descendants(SPAN);
                foreach (var span in spans)
                {
                    sb.AppendFormat("{0} ", span.InnerText.Trim());
                }
                var ingredient = sb.ToString().Trim();
                this.Ingredients.Add(ingredient);
            }
        }

        protected override void GetProcedures()
        {
            var node = base.GetNode("section", "recipe-steps-wrap");
            if (null != node)
            {
                this.GetDirections(node);
            }
        }

        private void GetDirections(HtmlNode parent)
        {
            var lis = parent.Descendants(LI);
            foreach (var li in lis)
            {
                var procedure = li.InnerText.Trim();
                this.Procedures.Add(procedure);
            }
        }
    }
}