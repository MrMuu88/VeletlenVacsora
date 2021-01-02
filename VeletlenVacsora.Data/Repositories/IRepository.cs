using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VeletlenVacsora.Data.Repositories
{
	public interface IRepository<T> where T : class
	{
		internal VacsoraDbContext DbContext { get; }

		//TODO Add Methods for Inlcudes
		Task<ICollection<T>> GetManyAsync(IEnumerable<int> ids = null);
		Task<T> GetAsync(int id);
		Task<ICollection<T>> FindAsync(Expression<Func<T,bool>> predicate);
		Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
		Task AddAsync(T entity);
		Task AddRangeAsync(IEnumerable<T> entities);
		Task DeleteAsync(T entity);
		Task UpdateAsync(T entity);
		Task CommitAsync();
		Task RevertAsync();
		Task<bool> Exist(int id);
		Task<IEnumerable<int>> ListAsync();
	}
}
