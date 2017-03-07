using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Domain
{
    public class ProcedureGroup
    {
		public string Text { get; set; }
		public HashSet<ProcedureGroupItem> Items { get; set; }


		public ProcedureGroup()
		{
			this.Items = new HashSet<ProcedureGroupItem>();
		}
		public ProcedureGroup(string text) 
			: this()
		{
			if (string.IsNullOrEmpty(text))
				throw new ArgumentException("parameter text is null or Empty.");

			this.Text = text;
		}



		public void Add(ProcedureGroupItem i)
		{
			if (null == i)
				throw new ArgumentNullException("parameter igiText is null or Empty.");

			this.Items.Add(i);
		}
	}//class
}//ns
