
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
		public IEnumerable<Recepie> GetAllRecepies() {
			return _dbContext.Recepies.Include(r => r.Category);

		}

		public Recepie GetRecepieByID(int id) {
			_dbContext.Categories.ToList();
			return _dbContext.Recepies.Where(r => r.ID == id).Include(r => r.Ingredients).ThenInclude(ri => ri.Ingredient).FirstOrDefault();
		}

		#endregion
	}
}
