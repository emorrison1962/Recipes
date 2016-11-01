using Recipes.Contracts.Repositories;
using Recipes.Contracts.Services;
using Recipes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Services
{
	public class TagService : IServiceBase<Tag>
	{
		IRepositoryBase<Tag> Repository { get; set; }

		public TagService(IRepositoryBase<Tag> r)
		{
			this.Repository = r;
		}

		public void Delete(Tag entity)
		{
			this.Repository.Delete(entity);
			this.Repository.Commit();
		}

		public void Delete(int id)
		{
			this.Repository.Delete(id);
			this.Repository.Commit();
		}

		public IEnumerable<Tag> GetAll()
		{
			var result = this.Repository.GetAll();
			return result;
		}

		public IEnumerable<Tag> GetAll(object filter)
		{
			throw new NotImplementedException();
		}

		public Tag GetById(int id)
		{
			var result = this.Repository.GetById(id);
			return result;
		}

		public Tag GetFullObject(object id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Tag> GetPaged(int top = 20, int skip = 0, object orderBy = null, object filter = null)
		{
			throw new NotImplementedException();
		}

		public Tag Insert(Tag entity)
		{
			this.Repository.Insert(entity);
			this.Repository.Commit();
			return entity;
		}

		public Tag Update(Tag entity)
		{
			this.Repository.Update(entity);
			this.Repository.Commit();
			return entity;
		}
	}
}
