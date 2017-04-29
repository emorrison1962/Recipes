using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Domain
{
	[Table("Tags")]
	public class Tag : EntityBase<Tag>
	{
		public int TagId { get; set; }
		public string Name { get; set; }

        public List<Recipe> Recipes { get; set; }

        public override int PrimaryKey
        {
            get
            {
                return TagId;
            }
        }

        public Tag()
        {
            this.Init();
        }
        void Init()
        {
        }

    }
}
