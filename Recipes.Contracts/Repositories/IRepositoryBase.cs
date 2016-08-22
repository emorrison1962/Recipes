using System.Linq;

namespace Recipes.Contracts.Repositories
{
	public interface IRepositoryBase<T>
	{
		void Commit();
		void Delete(T entity);
		void Delete(int id);
		void Dispose();
		IQueryable<T> GetAll();
		IQueryable<T> GetAll(object filter);
		T GetById(int id);
		T GetFullObject(object id);
		IQueryable<T> GetPaged(int top = 20, int skip = 0, object orderBy = null, object filter = null);
		void Insert(T entity);
		void Update(T entity);
	}
}