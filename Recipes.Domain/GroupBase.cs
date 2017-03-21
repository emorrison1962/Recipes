﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Domain
{
	public class GroupBase<T>
	{
		public string Text { get; set; }
		public HashSet<T> Items { get; set; }

	}
}
