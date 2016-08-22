using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Domain
{
	[Table("SubCategories")]
	public class SubCategory
	{
		public int SubCategoryId { get; private set; }
		public int CategoryId { get; private set; }

		public string Name { get; set; }
	}
}
