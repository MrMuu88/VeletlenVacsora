using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using VeletlenVacsora.Data.Exceptions;
using VeletlenVacsora.Data.Models;
using VeletlenVacsora.Data.Repositories;

namespace VeletlenVacsora.Data.Extensions
{
	public static class RecepiesRepositoryExtensions
	{
		public static async Task<RecepieModel> GetRandomAsync(this IRepository<RecepieModel> repo) {
			try
			{
				var dice = new Random();
				var recepies = await repo.DbContext.Recepies.ToArrayAsync();
				return recepies.OrderBy(r => r.Weight * dice.Next()).First();
			}
			catch (Exception ex)
			{
				throw new RepositoryException($"An exception occured when Executing {MethodBase.GetCurrentMethod().Name}", ex);
			}
		}
	}
}
