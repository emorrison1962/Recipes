using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Services
{
    public class PageParser
    {
		public string Parse(string url)
		{
			var result = this.GetTitle(url);
			return result;
		}

		string GetContent(string url)
		{
			var cli = new HttpClient();
			var response = cli.GetAsync(url).Result;
			var content = response.Content.ReadAsStringAsync().Result;

			return content;
		}

		string GetTitle(string url)
		{
			var html = this.GetContent(url);
			var doc = new HtmlDocument();
			doc.LoadHtml(html);

			var titleNode = doc.DocumentNode.Descendants("title").FirstOrDefault();
			string result = null;
			if (null != titleNode)
			{
				result = WebUtility.HtmlDecode(titleNode.InnerText);
				result = result.Trim();
			}
			return result;

		}

	}
}
