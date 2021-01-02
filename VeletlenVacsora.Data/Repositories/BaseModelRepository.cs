using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using VeletlenVacsora.Data.Exceptions;
using VeletlenVacsora.Data.Models;

namespace VeletlenVacsora.Data.Repositories
{

	public class BaseModelRepository<T> : IRepository<T> where T : BaseModel
	{
		internal VacsoraDbContext DbContext { get;  }

		VacsoraDbContext IRepository<T>.DbContext => DbContext;

		public BaseModelRepository(VacsoraDbContext dbContext)
		{
			DbContext = dbContext;
		}

		public async Task AddAsync(T entity)
		{
			try
			{
				await DbContext.Set<T>().AddAsync(entity);
			}
			catch (Exception ex)
			{
				throw new RepositoryException($"An exception occured when Executing {MethodBase.GetCurrentMethod().Name}", ex);
			}
		}

		public async Task AddRangeAsync(IEnumerable<T> entities)
		{
			try
			{
				await DbContext.Set<T>().AddRangeAsync(entities);
			}
			catch (Exception ex)
			{
				throw new RepositoryException($"An exception occured when Executing {MethodBase.GetCurrentMethod().Name}", ex);
			}
		}

		public async Task<IEnumerable<int>> ListAsync()
		{
			try
			{
				return await DbContext.Set<T>().Select(e => e.Id).ToArrayAsync();
			}
			catch (Exception ex)
			{
				throw new RepositoryException($"An exception occured when Executing {MethodBase.GetCurrentMethod().Name}", ex);
			}
		}

		public Task DeleteAsync(T entity)
		{
			//Remove is not async Command in EF but it's keeps consistent to call this method in an async way as well
			try
			{
				DbContext.Set<T>().Remove(entity);
				return Task.CompletedTask;
			}
			catch (Exception ex)
			{
				throw new RepositoryException($"An exception occured when Executing {MethodBase.GetCurrentMethod().Name}", ex);
			}
		}

		public async Task<ICollection<T>> GetManyAsync(IEnumerable<int> ids = null)
		{
			try
			{
				if (ids != null)
					return await DbContext.Set<T>().Where(e => ids.Contains(e.Id)).ToListAsync();
				else
					return await DbContext.Set<T>().ToListAsync();
			}
			catch (Exception ex)
			{
				throw new RepositoryException($"An exception occured when Executing {MethodBase.GetCurrentMethod().Name}", ex);
			}
		}

		public async Task<T> GetAsync(int id)
		{
			try
			{
				return await DbContext.Set<T>().FirstAsync(t => t.Id == id);
			}
			catch (Exception ex)
			{
				throw new RepositoryException($"An exception occured when Executing {MethodBase.GetCurrentMethod().Name}", ex);
			}
		}

		public Task UpdateAsync(T entity)
		{
			//Update is not async Command in EF but it's keeps consistent to call this method in an async way as well
			try
			{
				DbContext.Set<T>().Update(entity);

				return Task.CompletedTask;
			}
			catch (Exception ex)
			{
				throw new RepositoryException($"An exception occured when Executing {MethodBase.GetCurrentMethod().Name}", ex);
			}

		}

		public async Task<ICollection<T>> FindAsync(Expression<Func<T, bool>> predicate)
		{
			try
			{
				IQueryable<T> query = DbContext.Set<T>();
				return await query.Where(predicate).ToListAsync();
			}
			catch (Exception ex)
			{
				throw new RepositoryException($"An exception occured when Executing {MethodBase.GetCurrentMethod().Name}", ex);
			}
		}

		public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
		{
			try
			{
				IQueryable<T> query = DbContext.Set<T>();
				if (predicate != null)
					query = query.Where(predicate);
				return await query.CountAsync();
			}
			catch (Exception ex)
			{
				throw new RepositoryException($"An exception occured when Executing {MethodBase.GetCurrentMethod().Name}", ex);
			}
		}

		public async Task CommitAsync()
		{
			try
			{
				await DbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new RepositoryException($"An exception occured when Executing {MethodBase.GetCurrentMethod().Name}", ex);
			}
		}

		public Task RevertAsync()
		{
			try
			{
				var changed = DbContext.ChangeTracker.Entries<T>().Where(x => x.State != EntityState.Unchanged).ToList();
				foreach (var entry in changed)
				{
					switch (entry.State)
					{
						case EntityState.Modified:
							entry.CurrentValues.SetValues(entry.OriginalValues);
							entry.State = EntityState.Unchanged;
							break;
						case EntityState.Added:
							entry.State = EntityState.Detached;
							break;
						case EntityState.Deleted:
							entry.State = EntityState.Unchanged;
							break;
					}
				}
				return Task.CompletedTask;
			}
			catch (Exception ex)
			{
				throw new RepositoryException($"An exception occured when Executing {MethodBase.GetCurrentMethod().Name}", ex);
			}

		}

		public async Task<bool> Exist(int id)
		{
			try
			{
				return await DbContext.Set<T>().AnyAsync(t => t.Id == id);
			}
			catch (Exception ex)
			{
				throw new RepositoryException($"An exception occured when Executing {MethodBase.GetCurrentMethod().Name}", ex);
			}
		}
	}
}
