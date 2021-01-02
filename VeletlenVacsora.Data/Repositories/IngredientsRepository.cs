using Microsoft.EntityFrameworkCore;
using System.Linq;
using VeletlenVacsora.Data.Models;

namespace VeletlenVacsora.Data.Repositories
{
	public class IngredientsRepository : DefaultRepository<IngredientModel>
	{
		public IngredientsRepository(VacsoraDbContext dbContext) : base(dbContext) { }

		protected override IQueryable<IngredientModel> ApplyIncludes(IQueryable<IngredientModel> query)
		{
			return query.Include(i => i.IngredientType).Include(i => i.PackageType);
		}
	}
}
