using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Domain
{
	[Table("Tags")]
	public class Tag
	{
		public int TagId { get; private set; }
		public string Name { get; set; }

        public List<Recipe> Recipes { get; set; }
	}
}
