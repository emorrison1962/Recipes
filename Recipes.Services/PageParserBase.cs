using HtmlAgilityPack;
using Recipes.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Services
{
	public class PageParserBase
	{
        #region Fields

        protected const string DIV = "div";
        protected const string CLASS = "class";
        protected const string OL = "ol";
        protected const string UL = "ul";
        protected const string LI = "li";
        protected const string INGREDIENTS = "recipe-ingredients";
        protected const string PROCEDURES = "recipe-procedures";

        #endregion

        #region Properties

        protected string Title { get; set; }
        protected string ImageUrl { get; set; }
        protected Image Image { get; set; }
        protected List<string> Ingredients { get; set; }
        protected List<string> Procedures { get; set; }

        #endregion

        public PageParserBase()
        {
            this.Ingredients = new List<string>();
            this.Procedures = new List<string>();
        }

        virtual public Recipe TryParse(string url)
		{
            var result = new Recipe();
            bool success = false;

            var html = this.GetContent(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            success = this.GetTitle(doc);
            if (success)
            {
                success = false;
                success = this.GetImage(doc);
            }

            if (success)
            {
                success = false;
                success = this.GetIngredients(doc);
            }

            if (success)
            {
                success = false;
                success = this.GetProcedures(doc);
            }

            result = new Recipe()
            {
                Name = this.Title,
                Ingredients = this.Ingredients,
                Procedure = this.Procedures,
                Uri = url,
                ImageUri = this.ImageUrl,
                Source = new UriBuilder().Host
            };

            return result;
		}

        protected string GetContent(string url)
		{
            const string GZIP = "gzip";
            var cli = new HttpClient();
			var response = cli.GetAsync(url).Result;

			Debug.WriteLine(url);

			var content = string.Empty;
			if (response.Content.Headers.ContentEncoding.Contains(GZIP))
				content = UnGzip(response);
			else
				content = response.Content.ReadAsStringAsync().Result;


			return content;
		}

        protected string UnGzip(HttpResponseMessage response)
		{
			string result = string.Empty;
			using (var responseStream = response.Content.ReadAsStreamAsync().Result)
			{
				var zipStream = new GZipStream(responseStream, CompressionMode.Decompress);

				var reader = new StreamReader(zipStream, Encoding.Default);

				result = reader.ReadToEnd();

			}
			return result;
		}

        protected bool GetTitle(HtmlDocument doc)
		{
            const string TITLE = "title";

            bool result = false;
            var titleNode = doc.DocumentNode.Descendants(TITLE).FirstOrDefault();
			if (null != titleNode)
			{
				this.Title = WebUtility.HtmlDecode(titleNode.InnerText);
                this.Title = this.Title.Trim();
                result = true;
			}

			return result;

		}


        virtual protected bool GetIngredients(HtmlDocument doc)
        {
            var result = false;
            Debug.WriteLine(doc.ToString());

            //<div class="recipe-ingredients">

            var div = GetIngredientsDiv(doc);
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

        HtmlNode GetIngredientsDiv(HtmlDocument doc)
        {
            var divs = doc.DocumentNode.Descendants(DIV);
            var result = divs.Where(x => x.HasAttributes
                    && x.Attributes.Where(a => CLASS == a.Name && INGREDIENTS == a.Value).Count() > 0
                    ).FirstOrDefault();

            return result;
        }

        HtmlNode GetIngredientsList(HtmlNode div)
        {
            HtmlNode result = null;

            result = div.Descendants(UL).FirstOrDefault();
            if (null == result)
                result = div.Descendants(OL).FirstOrDefault();

            return result;
        }

        List<string> GetIngredients(HtmlNode list)
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


        virtual protected bool GetProcedures(HtmlDocument doc)
        {
            var result = false;
            Debug.WriteLine(doc.ToString());

            //<div class="recipe-ingredients">

            var div = GetProceduresDiv(doc);
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

        HtmlNode GetProceduresDiv(HtmlDocument doc)
        {
            var divs = doc.DocumentNode.Descendants(DIV);
            var result = divs.ByClass(INGREDIENTS).FirstOrDefault();
            throw new NotImplementedException("INGREDIENTS??");

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


        protected bool GetImage(HtmlDocument doc)
        {
            const string IMG = "img";
            const string SRC = "src";

            bool result = false;

            if (GetOpenGraphImage(doc))
            {
                result = true;
            }
            else
            {
                var images = doc.DocumentNode.Descendants(IMG);
                foreach (var image in images)
                {
                    var src = image.Attributes.FirstOrDefault(a => a.Name == SRC);
                    if (null != src)
                    {
                        this.ImageUrl = src.Value;
                        //this.GetImage(url);
                    }
                }
            }

            return result;
        }

        private bool GetOpenGraphImage(HtmlDocument doc)
        {
            //<meta property="og:image" content = "http://assets.epicurious.com/photos/561025b0f9a84192308aa2ca/1:1/w_600%2Ch_600/103005.jpg" />

            bool result = false;
            const string OG_IMAGE = "og:image";
            const string META = "meta";
            const string PROPERTY = "property";
            const string CONTENT = "content";

            var metas = doc.DocumentNode.Descendants(META);
            if (null != metas)
            {
                foreach (var meta in metas)
                {
                    var src = meta.Attributes.FirstOrDefault(a => a.Name == PROPERTY);
                    if (null != src)
                    {
                        if (src.Value == OG_IMAGE)
                        {
                            var content = meta.Attributes.FirstOrDefault(a => a.Name == CONTENT);
                            if (null != content)
                            {
                                this.ImageUrl = content.Value;
                                //this.GetImage(url);
                                result = true;
                                break;
                            }
                        }
                    }
                }
            }


            return result;
        }

        bool GetImage(string url)
        {

            string PATH_FORMAT = @"c:\temp\{0}.jpg";
            var result = false;
            var path = string.Format(PATH_FORMAT, Guid.NewGuid().ToString());

            try
            {
                HttpWebRequest imageRequest = (HttpWebRequest)WebRequest.Create(url);
                WebResponse imageResponse = imageRequest.GetResponse();
                Debug.WriteLine(imageResponse.ContentType);

                using (Stream responseStream = imageResponse.GetResponseStream())
                {

                    var tmp = Image.FromStream(responseStream);
                    var image = tmp.Clone();
                    this.Image = image as Image;
                    result = true;
                }

                Image.Save(path);

            }
            catch (System.UriFormatException)
            { //Invalid URI, swallow the exception.
            }
            catch (Exception)
            {
            }
            return result;
        }


    }//class

    public class SeriousEatsParser : PageParserBase
    {

        override public Recipe TryParse(string url)
        {
            var result = new Recipe();
            bool success = false;

            var html = this.GetContent(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            success = this.GetTitle(doc);
            if (success)
            {
                success = false;
                success = this.GetImage(doc);
            }

            if (success)
            {
                success = false;
                success = this.GetIngredients(doc);
            }

            if (success)
            {
                success = false;
                success = this.GetProcedures(doc);
            }

            result = new Recipe()
            {
                Name = this.Title,
                Ingredients = this.Ingredients,
                Procedure = this.Procedures,
                Uri = url,
                ImageUri = this.ImageUrl,
                Source = new UriBuilder().Host
            };

            return result;
        }

        bool GetIngredients(HtmlDocument doc)
        {
            var result = false;
            Debug.WriteLine(doc.ToString());

            //<div class="recipe-ingredients">

            var div = GetIngredientsDiv(doc);
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

        HtmlNode GetIngredientsDiv(HtmlDocument doc)
        {
            var divs = doc.DocumentNode.Descendants(DIV);
            var result = divs.Where(x => x.HasAttributes
                    && x.Attributes.Where(a => CLASS == a.Name && INGREDIENTS == a.Value).Count() > 0
                    ).FirstOrDefault();

            return result;
        }

        HtmlNode GetIngredientsList(HtmlNode div)
        {
            HtmlNode result = null;

            result = div.Descendants(UL).FirstOrDefault();
            if (null == result)
                result = div.Descendants(OL).FirstOrDefault();

            return result;
        }

        List<string> GetIngredients(HtmlNode list)
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


        bool GetProcedures(HtmlDocument doc)
        {
            var result = false;
            Debug.WriteLine(doc.ToString());

            //<div class="recipe-ingredients">

            var div = GetProceduresDiv(doc);
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

        HtmlNode GetProceduresDiv(HtmlDocument doc)
        {
            var divs = doc.DocumentNode.Descendants(DIV);
            var result = divs.Where(x => x.HasAttributes
                    && x.Attributes.Where(a => CLASS == a.Name && INGREDIENTS == a.Value).Count() > 0
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

    public class EpicurousParser : PageParserBase
    {

        public EpicurousParser()
        {

        }

        override protected bool GetIngredients(HtmlDocument doc)
        {
            var result = false;
            Debug.WriteLine(doc.ToString());

            //<div class="recipe-ingredients">

            var div = GetIngredientsDiv(doc);
            if (null != div)
            {
                var ingredientGroups = this.GetIngredientGroups(div);
                if (null != ingredientGroups)
                {
                    foreach (var ingredientGroup in ingredientGroups)
                    {
                        var ingredients = this.GetIngredients(ingredientGroup);
                        this.Ingredients.AddRange(ingredients);
                    }
                    if (this.Ingredients.Count > 0)
                        result = true;
                }
            }

            return result;
        }

        HtmlNode GetIngredientsDiv(HtmlDocument doc)
        {
            const string INGREDIENTS = "ingredients-info";
            var result = doc.DocumentNode.Descendants(DIV).ByClass(INGREDIENTS).FirstOrDefault(); 

            return result;
        }

        List<HtmlNode> GetIngredientGroups(HtmlNode div)
        {
            List<HtmlNode> result = null;

            var lis = div.Descendants(LI);
            result = lis.ByClass("ingredient-group").ToList();

            return result;
        }

        List<string> GetIngredients(HtmlNode ingredientGroup)
        {
            var result = new List<string>();

            if (null != ingredientGroup)
            {
                var strong = ingredientGroup.Descendants("strong").First();
                result.Add(strong.InnerText);

                foreach (var li in ingredientGroup.Descendants(LI))
                {
                    if (li.NodeType == HtmlNodeType.Element)
                    {
                        result.Add(li.InnerText);
                    }
                }
            }

            return result;
        }


        override protected bool GetProcedures(HtmlDocument doc)
        {
            var result = false;
            Debug.WriteLine(doc.ToString());

            //<div class="recipe-ingredients">

            var div = GetProceduresDiv(doc);
            if (null != div)
            {
                var preparationGroups = this.GetPreparationGroups(div);
                if (null != preparationGroups)
                {
                    foreach (var preparationGroup in preparationGroups)
                    {
                        var preparation = this.GetProcedures(preparationGroup);
                        this.Procedures.AddRange(preparation);
                    }
                    if (this.Procedures.Count > 0)
                        result = true;
                }
            }

            return result;
        }

        HtmlNode GetProceduresDiv(HtmlDocument doc)
        {
            var divs = doc.DocumentNode.Descendants(DIV);
            var result = divs.ByClass("instructions").FirstOrDefault();

            return result;
        }

        List<HtmlNode> GetPreparationGroups(HtmlNode div)
        {
            var result = div.Descendants(LI).ByClass("preparation-group").ToList();
            return result;
        }

        List<string> GetProcedures(HtmlNode preparationGroup)
        {
            var result = new List<string>();

            if (null != preparationGroup)
            {
                var strong = preparationGroup.Descendants("strong").First();
                result.Add(strong.InnerText);

                foreach (var li in preparationGroup.Descendants(LI))
                {
                    if (li.NodeType == HtmlNodeType.Element)
                    {
                        result.Add(li.InnerText.Trim());
                    }
                }
            }

            return result;
        }

    }//class

    public static class HtmlNodeExtensions
    {
        static public IEnumerable<HtmlNode> ByClass(this IEnumerable<HtmlNode> nodes, string classname)
        {
            var result = nodes.Where(x => x.HasAttributes
                    && x.Attributes.Where(a => "class" == a.Name && classname == a.Value).Count() > 0
                    );
            return result;
        }
    }

}//ns
