﻿using Recipes.Dal.Repositories;
using Recipes.DAL.Data;
using Recipes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.DAL.Repositories
{
	public class TagRepository : RepositoryBase<Tag>
	{
		public TagRepository(DataContext dataContext) : base (dataContext)
		{

		}
	}
}
