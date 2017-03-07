using Recipes.Contracts.Repositories;
using Recipes.Contracts.Services;
using Recipes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Recipes.Services
{
	public class RecipeService : IServiceBase<Recipe>
	{
		IRepositoryBase<Recipe> Repository { get; set; }

		public RecipeService(IRepositoryBase<Recipe> repository)
		{
			this.Repository = repository;
		}

		public IEnumerable<Recipe> GetAll()
		{
			var result = this.Repository.GetAll().ToList();
			result.Sort();
			return result;
		}

		public IEnumerable<Recipe> GetAll(object filter)
		{
			throw new NotImplementedException();
		}

		public Recipe Insert(Recipe recipe)
		{
			var result = this.Create(recipe.Uri);
			if (null != result)
			{
				this.Repository.Insert(result);
				this.Repository.Commit();
			}
			return result;
		}

		public Recipe Create(string url)
		{
			Recipe result = null;
			var parser = PageParserFactory.Create(url);

			result = parser.TryParse(url);
			return result;
		}

		public void Delete(Recipe entity)
		{
			throw new NotImplementedException();
		}
		public void Delete(int id)
		{
			try
			{
				this.Repository.Delete(id);
				this.Repository.Commit();
			}
#pragma warning disable 168
			catch (Exception ex)
			{
				throw;
			}
#pragma warning restore 168
		}

		public Recipe GetById(int id)
		{
			return this.Repository.GetById(id);
		}
		public Recipe GetFullObject(object id)
		{
			throw new NotImplementedException();
		}
		public IEnumerable<Recipe> GetPaged(int top = 20, int skip = 0, object orderBy = null, object filter = null)
		{
			throw new NotImplementedException();
		}
		public Recipe Update(Recipe entity)
		{
			try
			{
				this.Repository.Update(entity);
				this.Repository.Commit();

			}
#pragma warning disable 168
			catch (Exception ex)
			{
				throw;
			}
#pragma warning restore 168

			return this.Repository.GetById(entity.RecipeId);
		}


	}//class
}//ns
