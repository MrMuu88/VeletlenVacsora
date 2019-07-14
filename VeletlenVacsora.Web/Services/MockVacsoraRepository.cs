using System.Collections.Generic;
using VeletlenVacsora.Data;

namespace VeletlenVacsora.Web.Services {
	public class MockVacsoraRepository : IVacsoraRepository {
		public IEnumerable<Recepie> GetAllRecepies() {
			yield return new Recepie() { Name = "Paprikás krumpli" };
			yield return new Recepie() { Name = "Túrós csusza" };
			yield return new Recepie() { Name = "Pizza" };
			yield return new Recepie() { Name = "Bolognai" };
			yield return new Recepie() { Name = "Chilis bab" };
		}

		public Recepie GetRecepieByID(int id) {
			return new Recepie() { Name = "Csirkepörkölt" };
		}
	}
}
