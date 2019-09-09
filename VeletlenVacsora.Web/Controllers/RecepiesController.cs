using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using VeletlenVacsora.Services;
using VeletlenVacsora.Web.Models;

namespace VeletlenVacsora.Web.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class RecepiesController : ControllerBase {
		private IVacsoraRepository _repository;

		//TODO Implement Post Method
		//TODO Implement Put Method
		//TODO Implement Delete Method
		//TODO Impelement Async calls on Repository

		public RecepiesController(IVacsoraRepository repository) {
			_repository = repository;
		}


		[HttpGet]
		public ActionResult<ICollection<RecepieModel>> GetRecepies(string type = "") {
			try {
				var results = new List<RecepieModel>();
				if (!string.IsNullOrWhiteSpace(type)) {
					var raw = _repository.GetRecepiesByType(type);

					foreach (var r in raw) {
						results.Add(new RecepieModel(r));
					}
					return Ok(results);
				} else {
					foreach (var recepie in _repository.GetAllRecepies()) {
						results.Add(new RecepieModel(recepie));
					}
					return Ok(results);
				}

			} catch (Exception ex) {
				return StatusCode(StatusCodes.Status500InternalServerError, $"Server Failure: {ex.GetType().Name}\n{ex.Message}");
			}
		}




		[HttpGet("{ID}")]
		public ActionResult<RecepieModel> GetRecepieByID(int ID) {
			try {
				var result = _repository.GetRecepieByID(ID);

				return Ok(new RecepieModel(result));

			} catch (Exception ex) {
				return StatusCode(StatusCodes.Status500InternalServerError, $"Server Failure: {ex.GetType().Name}\n{ex.Message}");
			}
		}
	}
}