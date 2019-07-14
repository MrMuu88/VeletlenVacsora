using System.Collections.Generic;
using VeletlenVacsora.Data;

namespace VeletlenVacsora.Web.Services {
	public interface IVacsoraRepository {

		IEnumerable<Recepie> GetAllRecepies();
		Recepie GetRecepieByID(int id);
	}
}
