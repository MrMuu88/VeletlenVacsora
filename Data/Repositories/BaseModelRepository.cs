using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VeletlenVacsora.Data.Models;

namespace VeletlenVacsora.Data.Repositories
{
	//TODO ExceptionHandling
	//TODO Logging
	class BaseModelRepository<T> : IRepository<T> where T : BaseModel
	{
		public VacsoraDbContext DbContext { get; internal set; }
		public BaseModelRepository(VacsoraDbContext dbContext)
		{
			DbContext = dbContext;
		}

		public async Task<bool> AddAsync(T entity)
		{
			try
			{
				await DbContext.Set<T>().AddAsync(entity);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<bool> AddRangeAsync(IEnumerable<T> entities)
		{
			try
			{
				await DbContext.Set<T>().AddRangeAsync(entities);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public Task<bool> DeleteAsync(T entity)
		{
			//Remove is not async Command in EF but it's keeps consistent to call this method in an async way as well
			return Task.Run(() =>
			{
				try
				{
					DbContext.Set<T>().Remove(entity);
					return true;
				}
				catch
				{
					return false;
				}
			});
		}

		public async Task<ICollection<T>> GetAllAsync()
		{
			try
			{
				return await DbContext.Set<T>().ToListAsync();
			}
			catch
			{
				//returnning empty list on fail instead of null
				return new List<T>();
			}
		}

		public async Task<T> GetAsync(int id)
		{
			try
			{
				return await DbContext.Set<T>().FirstAsync(t => t.Id == id);
			}
			catch
			{
				return null;
			}
		}

		public Task<bool> UpdateAsync(T entity)
		{
			//Update is not async Command in EF but it's keeps consistent to call this method in an async way as well
			return Task.Run(() =>
				{
					try
					{
						DbContext.Set<T>().Update(entity);
						return true;
					}
					catch
					{
						return false;
					}
				}
			);
		}

		public async Task<ICollection<T>> FindAsync(Expression<Func<T, bool>> predicate)
		{
			try
			{
				IQueryable<T> query = DbContext.Set<T>();
				return await query.Where(predicate).ToListAsync();
			}
			catch
			{
				return new List<T>();
			}
		}

		public async Task<int?> CountAsync(Expression<Func<T, bool>> predicate = null)
		{
			try
			{
				IQueryable<T> query = DbContext.Set<T>();
				if (predicate != null)
					query = query.Where(predicate);
				return await query.CountAsync();
			}
			catch
			{
				return null;
			}
		}

		public async Task<bool> CommitAsync()
		{
			try
			{
				await DbContext.SaveChangesAsync();
				return true;
			}
			catch
			{

				throw;
			}
		}

		public Task<bool> RevertAsync()
		{
			return Task.Run(() =>
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
					return true;
				}
				catch
				{
					return false;
				}
			});
		}
	}
}
