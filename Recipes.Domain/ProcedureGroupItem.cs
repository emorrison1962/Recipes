using System.ComponentModel.DataAnnotations.Schema;

namespace Recipes.Domain
{
	public class ProcedureGroupItem : GroupItemBase
	{
		public int ProcedureGroupItemId { get; set; }
        public int? ProcedureGroupRefId { get; set; }

        [ForeignKey("ProcedureGroupRefId")]
        public virtual ProcedureGroup ProcedureGroup { get; set; }



		public ProcedureGroupItem()
		{

		}

		public ProcedureGroupItem(string text)
		{
			this.Text = text;
		}

        public override string ToString()
        {
            return base.ToString() + string.Format(", Text={0}", this.Text);
        }

    }//class
}//ns
