using HtmlAgilityPack;
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
	public class PageParser
	{
        public string Title { get; set; }
        public Image Image { get; set; }

		public bool TryParse(string url)
		{
            bool result = false;

            var html = this.GetContent(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            if (this.GetTitle(doc))
            {
                result = this.GetImage(doc);
            }

            return result;
		}

		string GetContent(string url)
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

		string UnGzip(HttpResponseMessage response)
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

		bool GetTitle(HtmlDocument doc)
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

        public string ImageUrl { get; set; }

        bool GetImage(HtmlDocument doc)
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

}//ns
