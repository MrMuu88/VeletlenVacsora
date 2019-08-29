
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


		public ICollection<Ingredient> GetAllIngredients() {
			throw new System.NotImplementedException();
		}


		public ICollection<Ingredient> GetIngredientsByRecepie() {
			throw new System.NotImplementedException();
		}

		public ICollection<Ingredient> GetIngredientsByType() {
			throw new System.NotImplementedException();
		}


		#endregion
	}
}
