using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Recipes.Domain
{
    [Serializable]
	public class ProcedureItem : GroupItemBase<ProcedureItem>
    {
		public int ProcedureItemId { get; set; }

        [JsonIgnore]
        [ForeignKey("ProcedureGroupId")]
        public virtual ProcedureGroup ProcedureGroup { get; set; }
        public virtual int? ProcedureGroupId { get; set; }

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

        [OnDeserialized]
        void OnDeserialized(StreamingContext ctx)
        {
            this.Init();
            if (null != this.ProcedureGroup)
            {
                this.ProcedureGroupId = this.ProcedureGroup.ProcedureGroupId;
                this.ProcedureGroup = null;
            }
        }
    }//class
}//ns
