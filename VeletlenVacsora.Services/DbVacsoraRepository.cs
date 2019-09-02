﻿
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using VeletlenVacsora.Data;

namespace VeletlenVacsora.Services {
	public class DbVacsoraRepository : IVacsoraRepository {
		private VacsoraDBContext _dbContext;

		#region fileds, Properties, events ########################################################

		#endregion

		#region Ctors #############################################################################
		public DbVacsoraRepository(string constr = "Data Source=..\\Vacsora.db;", DBType dBType = DBType.SqLite) {
			_dbContext = new VacsoraDBContext(constr, dBType);
		}


		#endregion

		#region Methods, Tasks ####################################################################
		public ICollection<Recepie> GetAllRecepies() {
			return _dbContext.Recepies.Include(r => r.Category).ToList();

		}
		public Recepie GetRecepieByID(int id) {
			_dbContext.Categories.ToList();
			return _dbContext.Recepies.Where(r => r.ID == id).Include(r => r.Ingredients).ThenInclude(ri => ri.Ingredient).FirstOrDefault();
		}

		public ICollection<Category> GetAllCategories() {
			return _dbContext.Categories.OrderBy(c => c.Type).ToList();
		}
		public ICollection<Category> GetCategoryByType(CategoryType type) {
			return _dbContext.Categories.Where(c => c.Type == type).ToList();
		}

		public Category GetCategoryByID(int ID) {
			return _dbContext.Categories.Where(c => c.ID == ID).FirstOrDefault();
		}

		//not nececarry
		public ICollection<Ingredient> GetAllIngredients() {
			return _dbContext.Ingredients.Include(i => i.IngredientType).Include(i => i.PackageType).ToList();
		}


		public ICollection<Ingredient> GetIngredientsByRecepie(int RecepieID) {
			throw new System.NotImplementedException();
		}

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


		#endregion
	}
}
