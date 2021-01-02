using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using VeletlenVacsora.Data.Models;
using VeletlenVacsora.Data.Repositories;

namespace VeletlenVacsora.Data.Extensions
{
	public static class CategoryRepositoryExtensions
	{
		public static async Task<CategoryModel> UpsertByNameAsync(this IRepository<CategoryModel> repo,string categoryName,CategoryType type) {
			var dbContext = repo.DbContext;
			var category = await dbContext.Categories.Where(c => c.Name == categoryName && c.Type == type).FirstOrDefaultAsync();
			if (category == null) {
				category = new CategoryModel(categoryName,type);
				dbContext.Add(category);
			}
			return category;
		}
	}
}
