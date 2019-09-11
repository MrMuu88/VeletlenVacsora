
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeletlenVacsora.Data;

namespace VeletlenVacsora.Services {
	public class DbVacsoraRepository : IVacsoraRepository {

		//TODO Implement Async Tasks

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


		public ICollection<Category> GetAllCategories() {
			return _dbContext.Categories.OrderBy(c => c.Type).ToList();
		}

		public ICollection<Category> GetCategoryByType(string type) {
			CategoryType QueryType;
			if (Enum.TryParse<CategoryType>(type, true, out QueryType)) {
				return _dbContext.Categories.Where(c => c.Type == QueryType).ToList();
			} else {
				return new List<Category>();
			}
		}

		public Category GetCategoryByID(int ID) {
			return _dbContext.Categories.Where(c => c.ID == ID).FirstOrDefault();
		}

		#endregion

		#region Ingredients ---------------------------------------------------

		public ICollection<Ingredient> GetAllIngredients() {
			return _dbContext.Ingredients.Include(i => i.IngredientType).Include(i => i.PackageType).ToList();
		}


		public ICollection<Ingredient> GetIngredientsByType(string type, string package) {
			IQueryable<Ingredient> query = _dbContext.Ingredients.Include(i => i.IngredientType).Include(i => i.PackageType);
			Category QueryType;
			Category QueryPackage;

			if (!string.IsNullOrWhiteSpace(type)) {

				QueryType = _dbContext.Categories.Where(c => c.Name.ToUpperInvariant() == type.ToUpperInvariant()).FirstOrDefault();
				if (QueryType != null) {
					query = query.Where(i => i.IngredientType == QueryType);
				} else {
					return new List<Ingredient>();
				}

			}
			if (!string.IsNullOrWhiteSpace(package)) {
				QueryPackage = _dbContext.Categories.Where(c => c.Name.ToUpperInvariant() == package.ToUpperInvariant()).FirstOrDefault();
				if (QueryPackage != null) {
					query = query.Where(i => i.PackageType == QueryPackage);
				} else {
					return new List<Ingredient>();
				}

			}

			return query.ToList();

		}

		public Ingredient GetIngredientByID(int ID) {
			return _dbContext.Ingredients.Where(i => i.ID == ID).Include(i => i.IngredientType).Include(i => i.PackageType).FirstOrDefault();
		}


		#endregion

		#region Common methods ------------------------------------------------

		public void Add<T>(T obj) where T : class {
			_dbContext.Add(obj);
		}

		//TODO Implement Delete<T>(obj)
		public void Delete<T>(T obj) where T : class {
			throw new NotImplementedException();
		}

		public async Task<bool> SaveChangesAsync() {
			return (await _dbContext.SaveChangesAsync()) > 0;

		}

		#endregion

		#endregion
	}
}
