using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Domain
{
    public class IngredientGroupItem : GroupItemBase
	{
		override public string Text {
			get
			{
				return this.Product.Text;
			}
			set { this.Product.Text = value; } }
		public Product Product { get; set; }
        public Amount Amount { get; set; }

		public IngredientGroupItem(string text)
		{
			this.Product = new Domain.Product(text);
		}
    }
}
