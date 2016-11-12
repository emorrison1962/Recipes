using HtmlAgilityPack;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Recipes.Services.Parsers
{
	class EatTenderParser : PageParserBase
	{
		protected override bool GetIngredients()
		{
			var result = false;
			var h3 = this.HtmlDocument.DocumentNode.Descendants("h3").Where(x => x.InnerText.ToLower() == "ingredients").First();
			var div = h3.ParentNode;
			if (null != div)
			{
				this.GetIngredients(div);
				if (this.Ingredients.Count > 0)
					result = true;
			}

			Debug.Assert(result);
			return result;
		}

		IEnumerable<HtmlNode> GetIngredients(HtmlNode div)
		{
			var result = new List<HtmlNode>();
			var children = div.Descendants(1);
			foreach (var child in children)
			{
				//Debug.WriteLine(child.Name);
				if (child.Name == "h4")
				{
					var ingredient = child.InnerText.Trim();
					this.Ingredients.Add(ingredient);
				}

				else if (child.Name == "ul")
				{
					var lis = child.Descendants(LI);
					foreach (var li in lis)
					{
						var ingredient = li.InnerText.Trim();
						this.Ingredients.Add(ingredient);
					}
				}
			}

			Debug.Assert(null != result);
			return result;
		}

		protected override bool GetProcedures()
		{
			var result = false;
			var h3 = this.HtmlDocument.DocumentNode.Descendants("h3").Where(x => x.InnerText.ToLower() == "directions").First();
			var div = h3.ParentNode;

			if (null != div)
			{
				this.GetProcedures(div);
				if (this.Procedures.Count > 0)
					result = true;
			}

			Debug.Assert(result);
			return result;
		}

		private void GetProcedures(HtmlNode div)
		{
			var result = new List<HtmlNode>();
			var children = div.Descendants(1);
			foreach (var child in children)
			{
				//Debug.WriteLine(child.Name);
				if (child.Name == "h4")
				{
					var procedure = child.InnerText.Trim();
					this.Procedures.Add(procedure);
				}

				else if (child.Name == "ol")
				{
					var lis = child.Descendants(LI);
					foreach (var li in lis)
					{
						var procedure = li.InnerText.Trim();
						this.Procedures.Add(procedure);
					}
				}
			}

			Debug.Assert(this.Procedures.Count > 0);
		}
	}//class
}//ns
