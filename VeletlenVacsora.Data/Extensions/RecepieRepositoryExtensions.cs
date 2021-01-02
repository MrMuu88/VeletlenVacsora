using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading.Tasks;
using VeletlenVacsora.Data.Exceptions;
using VeletlenVacsora.Data.Models;
using VeletlenVacsora.Data.Repositories;

namespace VeletlenVacsora.Data.Extensions
{
	public static class RecepiesRepositoryExtensions
	{
		public static async Task<RecepieModel> GetRandomAsync(this IRepository<RecepieModel> repo)
		{
			try
			{
				//TODO this Sql query can differ based on DB engine, create a Strategy pattern here if needed

				if (repo.DbContext.Database.IsSqlite()) { 
					//NOTE Doing this query with Linq, would cause the Whole table to be Queryied. this way, only a single entity will be returned

					var recepie = await repo.DbContext.Recepies.FromSqlRaw("SELECT * FROM Recepies ORDER BY (Recepies.Weight * RANDOM()) LIMIT 1").FirstOrDefaultAsync();
					return recepie;
				}
				return null;
			}
			catch (Exception ex)
			{
				throw new RepositoryException($"An exception occured when Executing {MethodBase.GetCurrentMethod().Name}", ex);
			}
		}
		public static async Task<int> RaiseWeightsAsync(this IRepository<RecepieModel> repo)
		{
			try
			{
				//NOTE Doing this query with Linq, would cause the Whole table to be Queryied.
				var RowsAffected = await repo.DbContext.Database.ExecuteSqlRawAsync($"UPDATE Recepies SET Weight = Weight + 1");
				return RowsAffected;
			}
			catch (Exception ex)
			{
				throw new RepositoryException($"An exception occured when Executing {MethodBase.GetCurrentMethod().Name}", ex);
			}
		}
	}
}
