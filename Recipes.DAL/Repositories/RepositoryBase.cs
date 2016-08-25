using Recipes.Contracts.Repositories;
using Recipes.DAL.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Dal.Repositories
{
	public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
	{
		internal DataContext _dataContext;
		internal DbSet<T> _dbSet;

		public RepositoryBase(DataContext dataContext)
		{
			this._dataContext = dataContext;
			this._dbSet = dataContext.Set<T>();
		}


		public virtual T GetById(int id)
		{
			return _dbSet.Find(id);
		}

		public virtual IEnumerable<T> GetAll()
		{
			return _dbSet;
		}

		public virtual IEnumerable<T> GetAll(object filter)
		{
			return null;
		}

		public virtual IEnumerable<T> GetPaged(int top = 20, int skip = 0, object orderBy = null, object filter = null)
		{
			return null;
		}

		public virtual T GetFullObject(object id)
		{
			return null;
		}

		public virtual void Insert(T entity)
		{
			_dbSet.Add(entity);
		}

		public virtual void Update(T entity)
		{
			_dbSet.Attach(entity);
			_dataContext.Entry(entity).State = EntityState.Modified;
		}

		public virtual void Delete(T entity)
		{
			if (_dataContext.Entry(entity).State == EntityState.Detached)
				_dbSet.Attach(entity);

			_dbSet.Remove(entity);
		}

		public virtual void Delete(int id)
		{
			var entity = _dbSet.Find(id);
			if (null != entity)
				this.Delete(entity);
		}

		public virtual void Commit()
		{
			_dataContext.SaveChanges();
		}

		public virtual void Dispose()
		{
			_dataContext.Dispose();
		}
	}
}
