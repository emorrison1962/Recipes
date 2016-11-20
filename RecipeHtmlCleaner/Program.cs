using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RecipeHtmlCleaner
{
	class Program
	{
		const string PATH = @"c:\temp";
		static void Main(string[] args)
		{
			new Program().MainImpl(args);
		}
		void MainImpl(string[] args)
		{
			var filenames = this.GetFilenames();
			foreach (var file in filenames)
			{
				this.Clean(file);
			}

		}

		const string SCRIPTS = @"<script([\s\S]*?)</script>";
		const string LINKS = @"<link([\s\S]*?)>";
		const string META = @"<meta([\s\S]*?)>";
		const string ANCHORS = @"<a.* href([\s\S]*?)</a>";

		private void Clean(string file)
		{
			string html = string.Empty;
			using (var reader = new StreamReader(file))
			{
				html = reader.ReadToEnd();
			}

			html = Regex.Replace(html, SCRIPTS, string.Empty);
			html = Regex.Replace(html, LINKS, string.Empty);
			html = Regex.Replace(html, META, string.Empty);
			html = Regex.Replace(html, ANCHORS, string.Empty);

			using (var writer = new StreamWriter(file, false))
			{
				writer.Write(html);
			}


		}

		List<string> GetFilenames()
		{
			var result = new List<string>(Directory.GetFiles(PATH));
			return result;
		}
	}//class
}//ns
