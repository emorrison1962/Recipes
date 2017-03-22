using Recipes.Contracts.Repositories;
using Recipes.Contracts.Services;
using Recipes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Recipes.Services
{
	public class RecipeService : ServiceBase<Recipe> 
	{
		IRepositoryBase<Recipe> Repository { get; set; }

		public RecipeService(IRepositoryBase<Recipe> repository)
            : base(repository)
		{
			this.Repository = repository;
		}

		override public IEnumerable<Recipe> GetAll()
		{
			var result = this.Repository.GetAll().ToList();
			result.Sort();
			return result;
		}

        override public IEnumerable<Recipe> GetAll(object filter)
		{
			throw new NotImplementedException();
		}

        override public Recipe Insert(Recipe recipe)
		{
            Recipe result = null;
            if (recipe.IsValid)
            {
                Func<Recipe, bool> where = x => x.Uri == recipe.Uri;
                var existing = Repository.GetAll(where).FirstOrDefault();
                if (null == existing)
                {
                    result = recipe;
                }
            }
            else
            {
                result = this.Create(recipe.Uri);
            }
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

        override public void Delete(Recipe entity)
		{
			throw new NotImplementedException();
		}
        override public void Delete(int id)
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

        override public Recipe GetById(int id)
		{
			return this.Repository.GetById(id);
		}
        override public Recipe GetFullObject(object id)
		{
			throw new NotImplementedException();
		}
        override public IEnumerable<Recipe> GetPaged(int top = 20, int skip = 0, object orderBy = null, object filter = null)
		{
			throw new NotImplementedException();
		}
        override public Recipe Update(Recipe entity)
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
