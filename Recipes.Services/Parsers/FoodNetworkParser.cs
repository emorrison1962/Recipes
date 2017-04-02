using HtmlAgilityPack;
using Recipes.Domain;

namespace Recipes.Services.Parsers
{
    class FoodNetworkParser : PageParserBase
    {
        protected override bool GetTitle()
        {
#if false
"<section class="o-AssetTitle">
  <h1 class="o-AssetTitle__a-Headline">
    <span class="o-AssetTitle__a-HeadlineText">Red Devil Cranberries</span>
  </h1>
</section>"
#endif
            bool result = false;
            var span = base.GetNode(SPAN, "o-AssetTitle__a-HeadlineText");
            if (null != span)
            {
                this.Title = span.InnerText;
                result = true;
            }
            return result;
        }
        protected override void GetIngredients()
        {
            var div = base.GetNode(DIV, "o-Ingredients__m-Body");
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
                var ingredient = li.InnerText.FromHtml().Trim();
                if (!string.IsNullOrEmpty(ingredient))
                    this.AddIngredient(ingredient);
            }
        }

        protected override void GetProcedures()
        {
            var div = base.GetNode(DIV, "o-Method__m-Body");
            if (null != div)
            {
                this.GetDirections(div);
            }
        }

        private void GetDirections(HtmlNode div)
        {
            var ps = div.Descendants("p");
            foreach (var p in ps)
            {
                var procedure = p.InnerText.FromHtml().Trim();
                if (!string.IsNullOrEmpty(procedure))
                    this.Add(new ProcedureItem(procedure));
            }
        }
    }
}
