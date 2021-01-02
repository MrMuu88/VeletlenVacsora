using Microsoft.EntityFrameworkCore;
using System.Linq;
using VeletlenVacsora.Data.Models;

namespace VeletlenVacsora.Data.Repositories
{
	public class RecepiesRepository : DefaultRepository<RecepieModel>
	{
		public RecepiesRepository(VacsoraDbContext dbContext) : base(dbContext){}

		protected override IQueryable<RecepieModel> ApplyIncludes(IQueryable<RecepieModel> query)
		{
			return query.Include(r => r.Category)
				.Include(r => r.Ingredients).ThenInclude(ri => ri.Ingredient).ThenInclude(i => i.IngredientType)
				.Include(r => r.Ingredients).ThenInclude(ri => ri.Ingredient).ThenInclude(i => i.PackageType);
		}
	}
}
