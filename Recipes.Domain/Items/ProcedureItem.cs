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

        public override int PrimaryKey
        {
            get
            {
                return ProcedureItemId;
            }
        }

        public ProcedureItem()
		{
            this.Init();
		}

		public ProcedureItem(string text)
		{
			this.Text = text;
            this.Init();
        }
        void Init()
        {
        }

    }//class
}//ns
