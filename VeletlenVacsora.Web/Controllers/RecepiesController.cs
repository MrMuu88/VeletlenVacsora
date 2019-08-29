using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VeletlenVacsora.Services;
using VeletlenVacsora.Web.Models;

namespace VeletlenVacsora.Web.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class RecepiesController : ControllerBase {
		private IVacsoraRepository _repository;

		public RecepiesController(IVacsoraRepository repository) {
			_repository = repository;
		}

		[HttpGet]
		public IEnumerable<RecepieModel> GetAllRecepies() {
			var results = new List<RecepieModel>();

			foreach (var recepie in _repository.GetAllRecepies()) {
				results.Add(new RecepieModel(recepie));
			}

			return results;
		}


		[HttpGet("{ID}")]
		public RecepieModel GetRecepieByID(int ID) {
			var result = _repository.GetRecepieByID(ID);
			return new RecepieModel(result);
		}
	}
}