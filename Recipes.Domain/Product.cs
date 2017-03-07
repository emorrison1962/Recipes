namespace Recipes.Domain
{
	public class Product
	{
		public string Text { get; set; }

		public Product(string text)
		{
			this.Text = text;
		}
	}
}
