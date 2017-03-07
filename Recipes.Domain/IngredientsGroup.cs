using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Domain
{
    public class IngredientGroup
    {
		public int IngredientGroupId { get; set; }
		public string Text { get; set; }
		public HashSet<IngredientGroupItem> Items { get; set; }

		public IngredientGroup()
		{
			this.Items = new HashSet<IngredientGroupItem>();
		}
		public IngredientGroup(string text)
			: this()
		{
			if (string.IsNullOrEmpty(text))
				throw new ArgumentException("parameter text is null or Empty.");

			this.Text = text;
		}

		public void Add(IngredientGroupItem i)
		{
			if (null == i)
				throw new ArgumentNullException("IngredientGroupItem i");

			this.Items.Add(i);
		}

		public void Add(string igiText)
		{
			if (string.IsNullOrEmpty(igiText))
				throw new ArgumentException("parameter igiText is null or Empty.");

			var i = new IngredientGroupItem(igiText);
			this.Add(i);
		}

	}
}
