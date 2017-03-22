using HtmlAgilityPack;
using Recipes.Domain;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Recipes.Services.Parsers
{
	class EatTenderParser : PageParserBase
	{
		protected override void GetIngredients()
		{
			var h3 = this.HtmlDocument.DocumentNode.Descendants("h3").Where(x => x.InnerText.FromHtml().ToLower() == "ingredients").First();
			var div = h3.ParentNode;
			if (null != div)
			{
				this.GetIngredients(div);
			}
		}

		void GetIngredients(HtmlNode div)
		{
			var children = div.Descendants(1);
			foreach (var child in children)
			{
				//Debug.WriteLine(child.Name);
				if (child.Name == "h4")
				{
					var ingredient = child.InnerText.FromHtml();
					this.Add(new IngredientGroup(ingredient));
				}

				else if (child.Name == "ul")
				{
					var lis = child.Descendants(LI);
					foreach (var li in lis)
					{
						var ingredient = li.InnerText.FromHtml();
						this.AddIngredient(ingredient);
					}
				}
			}
		}

		protected override void GetProcedures()
		{
			var h3 = this.HtmlDocument.DocumentNode.Descendants("h3").Where(x => x.InnerText.Trim().ToLower() == "directions").First();
			var div = h3.ParentNode;

			if (null != div)
			{
				this.GetProcedures(div);
			}
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
					var procedure = child.InnerText.FromHtml();
					this.Add(new ProcedureGroup(procedure));
				}

				else if (child.Name == "ol")
				{
					var lis = child.Descendants(LI);
					foreach (var li in lis)
					{
						var procedure = li.InnerText.FromHtml();
						this.Add(new ProcedureItem(procedure));
					}
				}
			}

			Debug.Assert(this.ProcedureGroups.Count > 0);
		}
	}//class
}//ns
