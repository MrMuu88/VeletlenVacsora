
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeletlenVacsora.Data;

namespace VeletlenVacsora.Services {
	public class DbVacsoraRepository : IVacsoraRepository {

		#region fileds, Properties, events ########################################################

		private VacsoraDBContext _dbContext;

		#endregion

		#region Ctors #############################################################################
		public DbVacsoraRepository(string constr = "Data Source=..\\Vacsora.db;", DBType dBType = DBType.SqLite) {
			_dbContext = new VacsoraDBContext(constr, dBType);
		}
		#endregion

		#region Methods, Tasks ####################################################################


		#region Recepies ------------------------------------------------------
		public async Task<ICollection<Recepie>> GetAllRecepiesAsync() {
			return await _dbContext.Recepies.Include(r => r.Category).ToListAsync();
		}


		public async Task<ICollection<Recepie>> GetRecepiesByTypeAsync(string type) {
			var QueryType = _dbContext.Categories.Where(c => EF.Functions.Like(c.Name, type)).FirstOrDefault();
			if (QueryType != null) {
				return await _dbContext.Recepies.Where(r => r.Category == QueryType).ToListAsync();
			} else {
				return new List<Recepie>();
			}
		}

		public async Task<Recepie> GetRecepieByIDAsync(int id) {
			_dbContext.Categories.ToList();
			return await _dbContext.Recepies.Where(r => r.ID == id).Include(r => r.Ingredients).ThenInclude(ri => ri.Ingredient).FirstOrDefaultAsync();
		}

		#endregion

		#region Categories ----------------------------------------------------


		public async Task<ICollection<Category>> GetAllCategoriesAsync() {
			return await _dbContext.Categories.OrderBy(c => c.Type).ToListAsync();
		}

		public async Task<ICollection<Category>> GetCategoryByTypeAsync(string type) {
			CategoryType QueryType;
			if (Enum.TryParse<CategoryType>(type, true, out QueryType)) {
				return await _dbContext.Categories.Where(c => c.Type == QueryType).ToListAsync();
			} else {
				return new List<Category>();
			}
		}

		public async Task<Category> GetCategoryByIDAsync(int ID) {
			return await _dbContext.Categories.Where(c => c.ID == ID).FirstOrDefaultAsync();
		}
		public async Task<Category> GetCategoryByNameAsync(string name) {
			return await _dbContext.Categories.Where(c => EF.Functions.Like(name, c.Name)).FirstOrDefaultAsync();
		}

		#endregion

		#region Ingredients ---------------------------------------------------

		public async Task<ICollection<Ingredient>> GetAllIngredientsAsync() {
			return await _dbContext.Ingredients.Include(i => i.IngredientType).Include(i => i.PackageType).ToListAsync();
		}


		public async Task<ICollection<Ingredient>> GetIngredientsByTypeAsync(string type, string package) {
			IQueryable<Ingredient> query = _dbContext.Ingredients.Include(i => i.IngredientType).Include(i => i.PackageType);
			Category QueryType;
			Category QueryPackage;

			if (!string.IsNullOrWhiteSpace(type)) {

				QueryType = await _dbContext.Categories.Where(c => c.Name.ToUpperInvariant() == type.ToUpperInvariant()).FirstOrDefaultAsync();
				if (QueryType != null) {
					query = query.Where(i => i.IngredientType == QueryType);
				} else {
					return new List<Ingredient>();
				}

			}
			if (!string.IsNullOrWhiteSpace(package)) {
				QueryPackage = await _dbContext.Categories.Where(c => c.Name.ToUpperInvariant() == package.ToUpperInvariant()).FirstOrDefaultAsync();
				if (QueryPackage != null) {
					query = query.Where(i => i.PackageType == QueryPackage);
				} else {
					return new List<Ingredient>();
				}

			}

			return await query.ToListAsync();

		}

		public async Task<Ingredient> GetIngredientByIDAsync(int ID) {
			return await _dbContext.Ingredients.Where(i => i.ID == ID).Include(i => i.IngredientType).Include(i => i.PackageType).FirstOrDefaultAsync();
		}


		#endregion

		#region Common methods ------------------------------------------------

		public async Task<bool> Add<T>(T obj) where T : class {
			_dbContext.Add(obj);
			return (await _dbContext.SaveChangesAsync()) > 0;
		}

		public async Task<bool> AddRange<T>(IEnumerable<T> objs) where T:class{
			foreach (T obj in objs) {
				_dbContext.Add(obj);
			}
			return (await _dbContext.SaveChangesAsync()) > 0;
		}
		public async Task<bool> Delete<T>(T obj) where T : class {
			_dbContext.Remove(obj);
			return (await _dbContext.SaveChangesAsync()) > 0;
		}

		public async Task<bool> Update<T>(T obj) where T : class {
			_dbContext.Update(obj);
			return (await _dbContext.SaveChangesAsync()) > 0;
		}

		#endregion

		#endregion
	}
}
