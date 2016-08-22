using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Domain
{
	[Table("Ethnicities")]
	public class Ethnicity
	{
		public int EthnicityId { get; set; }
		public string Name { get; set; }
	}
}
