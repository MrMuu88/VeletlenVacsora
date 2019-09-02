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
				foreach (var i in _repository.GetIngredientsByType(type, package)) {
					results.Add(new IngredientModel(i));
				}
				return Ok(results);

			} catch (Exception) {
				return StatusCode(StatusCodes.Status500InternalServerError, "DataBase Failure");
			}
		}
	}
}