using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VeletlenVacsora.Data.Repositories
{
	public interface IRepository<T> where T : class
	{
		//TODO Add Methods for Inlcudes
		Task<ICollection<T>> GetAllAsync();
		Task<T> GetAsync(int id);
		Task<ICollection<T>> FindAsync(Expression<Func<T,bool>> predicate);
		Task<int?> CountAsync(Expression<Func<T, bool>> predicate = null);
		Task<bool> AddAsync(T entity);
		Task<bool> AddRangeAsync(IEnumerable<T> entities);
		Task<bool> DeleteAsync(T entity);
		Task<bool> UpdateAsync(T entity);
		Task<bool> CommitAsync();
		Task<bool> RevertAsync();
		Task<bool> Exist(int id);
	}
}
