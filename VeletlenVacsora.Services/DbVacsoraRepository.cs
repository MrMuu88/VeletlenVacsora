
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
		public ICollection<Recepie> GetAllRecepies() {
			return _dbContext.Recepies.Include(r => r.Category).ToList();

		}

		public ICollection<Recepie> GetRecepiesByType() {
			throw new System.NotImplementedException();
		}

		public Recepie GetRecepieByID(int id) {
			_dbContext.Categories.ToList();
			return _dbContext.Recepies.Where(r => r.ID == id).Include(r => r.Ingredients).ThenInclude(ri => ri.Ingredient).FirstOrDefault();
		}

		#endregion

		#region Categories ----------------------------------------------------


		public ICollection<Category> GetAllCategories() {
			return _dbContext.Categories.OrderBy(c => c.Type).ToList();
		}

		//TODO Throw an exception when category not found
		public ICollection<Category> GetCategoryByType(string type = "") {
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

		//TODO use regexed match in the WHERE clauses
		public ICollection<Ingredient> GetIngredientsByType(string type = "", string package = "") {
			IQueryable<Ingredient> query = _dbContext.Ingredients.Include(i => i.IngredientType).Include(i => i.PackageType);

			if (!string.IsNullOrWhiteSpace(type)) {
				var QueryType = _dbContext.Categories.Where(c => c.Name == type).FirstOrDefault();
				if (QueryType != null) {
					query = query.Where(i => i.IngredientType == QueryType);
				}
			}
			if (!string.IsNullOrWhiteSpace(package)) {
				var QueryPackage = _dbContext.Categories.Where(c => c.Name == package).FirstOrDefault();
				if (QueryPackage != null) {
					query = query.Where(i => i.PackageType == QueryPackage);
				}
			}
			return query.ToList();
		}

		public Ingredient GetIngredientByID(int ID) {
			throw new System.NotImplementedException();
		}

		#endregion

		#endregion
	}
}
