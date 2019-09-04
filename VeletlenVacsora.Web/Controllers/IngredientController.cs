using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using VeletlenVacsora.Services;
using VeletlenVacsora.Web.Models;

namespace VeletlenVacsora.Web.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class IngredientController : ControllerBase {
		private readonly IVacsoraRepository _repository;

		public IngredientController(IVacsoraRepository repository) {
			_repository = repository;
		}

		[HttpGet]
		public ActionResult<ICollection<IngredientModel>> GetIngredients(string type = "", string package = "") {
			try {
				var results = new List<IngredientModel>();
				var raw = _repository.GetIngredientsByType(type, package);
				foreach (var i in raw) {
					results.Add(new IngredientModel(i));
				}
				return Ok(results);

			} catch (Exception ex) {
				return StatusCode(StatusCodes.Status500InternalServerError, $"Server Failure: {ex.GetType().Name}\n{ex.Message}");
			}
		}

		[HttpGet("{ID}")]
		public ActionResult<IngredientModel> GetIngredientByID(int ID) {
			try {
				var raw = _repository.GetIngredientByID(ID);
				if (raw != null) {
					return Ok(new IngredientModel(raw));
				} else {
					return NotFound();
				}

			} catch (Exception ex) {
				return StatusCode(StatusCodes.Status500InternalServerError, $"Server Failure: {ex.GetType().Name}\n{ex.Message}");
			}
		}
	}
}