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
using System.Web;

namespace Recipes.Services
{
    abstract public class PageParserBase
    {
        #region Fields

        protected const string SPAN = "span";
        protected const string DIV = "div";
        protected const string SECTION = "section";
        protected const string CLASS = "class";
        protected const string OL = "ol";
        protected const string UL = "ul";
        protected const string LI = "li";
        protected const string P = "p";
        protected const string INGREDIENTS = "recipe-ingredients";
        protected const string PROCEDURES = "recipe-procedures";

        #endregion

        #region Properties

        protected string Title { get; set; }
        protected string SourceUrl { get; set; }
        protected string ImageUrl { get; set; }
        protected Image Image { get; set; }
        protected List<IngredientGroup> IngredientGroups { get; set; }
        protected List<ProcedureGroup> ProcedureGroups { get; set; }
        protected HtmlDocument HtmlDocument { get; set; }

        #endregion

        public PageParserBase()
        {
            this.IngredientGroups = new List<IngredientGroup>();
            this.ProcedureGroups = new List<ProcedureGroup>();
        }

        virtual public Recipe TryParse(string url)
        {
            this.SourceUrl = url;
            var result = new Recipe();
            bool success = false;

            var html = this.GetContent(url);
            this.HtmlDocument = new HtmlDocument();
            this.HtmlDocument.LoadHtml(html);
            Debug.Assert(null != this.HtmlDocument);

            this.HtmlDocument.Save(string.Format(@"c:\temp\{0}.html", new UriBuilder(url).Host));

            //Debug.WriteLine(this.HtmlDocument.DocumentNode.InnerHtml);

            success = this.GetTitle();
            if (success)
            {
                success = false;
                success = this.GetImage();
            }

            if (success)
            {
                success = false;
                this.GetIngredients();
                if (0 < this.IngredientGroups.Count)
                {
                    success = true;
                }
            }

            if (success)
            {
                success = false;
                this.GetProcedures();
                if (0 < this.ProcedureGroups.Count)
                {
                    success = true;
                }
            }


            result = new Recipe()
            {
                Name = this.Title,
                IngredientGroups = this.IngredientGroups,
                ProcedureGroups = this.ProcedureGroups,
                Uri = url,
                ImageUri = this.ImageUrl,
                Source = new UriBuilder().Host
            };

            return result;
        }

        protected string GetContent(string url)
        {
            var result = string.Empty;
            try
            {
                HttpClientHandler handler = new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                };

                using (var cli = new HttpClient(handler))
                {
                    cli.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/json");
                    cli.Timeout = TimeSpan.FromSeconds(60);
                    using (var response = cli.GetAsync(url).Result)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            result = response.Content.ReadAsStringAsync().Result;
                        }
                        else
                        {
                            throw new Exception("Error Getting Url.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.Message);
                throw;
            }

            return result;
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

        virtual protected bool GetTitle()
        {
            const string TITLE = "title";

            bool result = false;
            var titleNode = this.HtmlDocument.DocumentNode.Descendants(TITLE).FirstOrDefault();
            if (null != titleNode)
            {
                this.Title = WebUtility.HtmlDecode(titleNode.InnerText.FromHtml());
                this.Title = this.Title.Trim();
                result = true;
            }

            return result;

        }

        abstract protected void GetIngredients();
        abstract protected void GetProcedures();

        virtual protected HtmlNode GetNode(string nodeType, string className)
        {
            var nodes = this.HtmlDocument.DocumentNode.Descendants(nodeType);
            var result = nodes.ByClass(className).FirstOrDefault();

            return result;
        }

        virtual protected List<HtmlNode> GetNodes(string nodeType, string className)
        {
            var nodes = this.HtmlDocument.DocumentNode.Descendants(nodeType);
            var result = nodes.ByClass(className).ToList();

            return result;
        }

        virtual protected HtmlNode GetNode(HtmlNode parent, string nodeType, string className)
        {
            var nodes = parent.Descendants(nodeType);
            var result = nodes.ByClass(className).FirstOrDefault();

            return result;
        }

        virtual protected List<HtmlNode> GetNodes(HtmlNode parent, string nodeType, string className)
        {
            var nodes = parent.Descendants(nodeType);
            var result = nodes.ByClass(className).ToList();

            return result;
        }


        protected bool GetImage()
        {
            const string IMG = "img";
            const string SRC = "src";

            bool result = false;

            if (GetOpenGraphImage())
            {
                result = true;
            }
            else
            {
                var images = this.HtmlDocument.DocumentNode.Descendants(IMG);
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

        private bool GetOpenGraphImage()
        {
            //<meta property="og:image" content = "http://assets.epicurious.com/photos/561025b0f9a84192308aa2ca/1:1/w_600%2Ch_600/103005.jpg" />

            bool result = false;
            const string OG_IMAGE = "og:image";
            const string META = "meta";
            const string PROPERTY = "property";
            const string CONTENT = "content";

            var metas = this.HtmlDocument.DocumentNode.Descendants(META);
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
                //Debug.WriteLine(imageResponse.ContentType);

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

        protected void Add(IngredientGroup g)
        {
            if (null == g)
                throw new ArgumentNullException("IngredientGroup g");
            this.IngredientGroups.Add(g);
        }

        protected void Add(ProcedureGroup g)
        {
            if (null == g)
                throw new ArgumentNullException("ProcedureGroup g");
            this.ProcedureGroups.Add(g);
        }

        protected void AddIngredient(string igiText)
        {
            if (string.IsNullOrEmpty(igiText))
                throw new ArgumentException("parameter igiText is null or Empty.");
            var i = new IngredientGroupItem(igiText);
            this.Add(i);
        }
        protected void Add(IngredientGroupItem i)
        {
            if (null == i)
                throw new ArgumentNullException("IngredientGroupItem i");

            var g = this.IngredientGroups.LastOrDefault();
            if (null == g)
            {
                g = new IngredientGroup();
                this.IngredientGroups.Add(g);
            }
            g.Add(i);
        }

        protected void Add(ProcedureGroupItem i)
        {
            if (null == i)
                throw new ArgumentNullException("ProcedureGroupItem i");

            var g = this.ProcedureGroups.LastOrDefault();
            if (null == g)
            {
                g = new ProcedureGroup();
                this.ProcedureGroups.Add(g);
            }
            g.Add(i);
        }

    }//class

    public class PageParsingException : Exception
    {
        public PageParsingException(string msg)
            : base(msg)
        {
        }
    }

    public static class HtmlNodeExtensions
    {
        static public IEnumerable<HtmlNode> ByClass(this IEnumerable<HtmlNode> nodes, string classname)
        {
            var seq = nodes.Where(x => x.HasAttributes);
            var result = seq.Where(x =>
                x.Attributes.Where(a => "class" == a.Name && a.Value.Contains(classname)).FirstOrDefault() != null);

#if false
			foreach (var div in seq)
			{
				foreach (var att in div.Attributes)
				{
					if (att.Name == "class")
					{
						//Debug.WriteLine(att.Value);
						if (att.Value.IndexOf("ingredient") > 0)
						{
							new object();
						}
					}
				}
			}

#endif
            return result;
        }
        static public IEnumerable<HtmlNode> ByID(this IEnumerable<HtmlNode> nodes, string id)
        {
            var seq = nodes.Where(x => x.HasAttributes);
            var result = seq.Where(x =>
                x.Attributes.Where(a => "id" == a.Name && a.Value.Contains(id)).FirstOrDefault() != null);

#if false
			foreach (var div in seq)
			{
				foreach (var att in div.Attributes)
				{
					if (att.Name == "id")
					{
						Debug.WriteLine(att.Value);
						if (att.Value.IndexOf("ingredient") > 0)
						{
							new object();
						}
					}
				}
			}

#endif
            return result;
        }

        static public string FromHtml(this string innerText)
        {
            var result = HttpUtility.HtmlDecode(innerText);
            result = result.Trim();
            return result;
        }

        static public HtmlNode GetNode(this HtmlNode parent, string nodeType, string className = null)
        {
            var result = parent.GetNodes(nodeType, className).FirstOrDefault();
            return result;
        }

        static public List<HtmlNode> GetNodes(this HtmlNode parent, string nodeType, string className = null)
        {
            var result = parent.Descendants(nodeType).ToList();
            if (null != className)
            {
                result = result.ByClass(className).ToList();
            }

            return result;
        }

        #region HtmlDocument Extensions

        static public HtmlNode GetNode(this HtmlDocument doc, string nodeType, string className = null)
        {
            var result = doc.GetNodes(nodeType, className).FirstOrDefault();
            return result;
        }

        static public List<HtmlNode> GetNodes(this HtmlDocument doc, string nodeType, string className = null)
        {
            var result = doc.DocumentNode.Descendants(nodeType).ToList();
            if (null != className)
            {
                result = result.ByClass(className).ToList();
            }
            return result;
        }

        #endregion

    }//class

}//ns
