using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Domain
{
	public class Recipe
	{
		public int RecipeId { get; private set; }
		public string Name { get; set; }
		public string Uri { get; set; }
		public string Source { get; set; }
		public List<Tag> Tags { get; set; }

		public int? EthnicityId { get; set; }
		public int? Rating { get; set; }
		public TimeSpan? Time { get; set; }

        public string ImageUri { get; set; }

        public List<string> Ingredients { get; set; }
        public List<string> Procedure { get; set; }

        public bool IsValid
        {
            get
            {
                var result = false;

                result = (null != Ingredients && Ingredients.Count > 0);

                if (result)
                    result = (null != Procedure && Procedure.Count > 0);

                if (result)
                    result = !string.IsNullOrEmpty(Name);

                if (result)
                    result = !string.IsNullOrEmpty(ImageUri);

                if (result)
                    result = !string.IsNullOrEmpty(Source);


                return result;
            }
        }
    }
}
