using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Recipes.Domain
{
    [Serializable]
	public class ProcedureItem : GroupItemBase<ProcedureItem>
    {
		public int ProcedureItemId { get; set; }

        [JsonIgnore][XmlIgnore]
        public virtual ProcedureGroup ProcedureGroup { get; set; }

		public ProcedureItem()
		{

		}

		public ProcedureItem(string text)
		{
			this.Text = text;
		}


    }//class
}//ns
