using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VeletlenVacsora.Data.Repositories
{
	interface IRepository<T> where T : class
	{
		Task<ICollection<T>> GetAllAsync();
		Task<T> GetAsync();
		Task<bool> AddAsync(T entity);
		Task<bool> AddRangeAsync(IEnumerable<T> entities);
		Task<bool> DeleteAsync(T entity);
		Task<bool> UpdateAsync(T entity);
	}
}
