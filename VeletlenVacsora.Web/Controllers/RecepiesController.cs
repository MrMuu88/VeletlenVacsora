using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VeletlenVacsora.Data;
using VeletlenVacsora.Services;

namespace VeletlenVacsora.Web.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class RecepiesController : ControllerBase {
		private IVacsoraRepository _repository;

		public RecepiesController(IVacsoraRepository repository) {
			_repository = repository;
		}

		[HttpGet]
		public IEnumerable<Recepie> GetAllRecepies() {
			var results = _repository.GetAllRecepies();
			return results;
		}


		[HttpGet("{ID}")]
		public Recepie GetRecepieByID(int ID) {
			var result = _repository.GetRecepieByID(ID);
			return result;
		}
	}
}