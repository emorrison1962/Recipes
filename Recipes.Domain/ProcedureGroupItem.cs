namespace Recipes.Domain
{
	public class ProcedureGroupItem : GroupItemBase
	{
		public int ProcedureGroupItemId { get; set; }

		public ProcedureGroupItem()
		{

		}

		public ProcedureGroupItem(string text)
		{
			this.Text = text;
		}
	}
}
