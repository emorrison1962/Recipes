using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Domain
{
    public class IngredientGroupItem : GroupItemBase
	{
		public int IngredientGroupItemId { get; set; }

		//override public string Text {
		//	get
		//	{
		//		return this.Product.Text;
		//	}
		//	set { this.Product.Text = value; } }

		[NotMapped]
		public Product Product { get; set; }
		[NotMapped]
		public Amount Amount { get; set; }

		public IngredientGroupItem()
		{

		}
		public IngredientGroupItem(string text)
		{
            this.Text = text;
            //this.Product = new Domain.Product(text);
		}
        public override string ToString()
        {
            return base.ToString() + string.Format(", Text={0}", this.Text);
        }

    }//class
}//ns
