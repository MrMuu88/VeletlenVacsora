using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeletlenVacsora.Services;
using VeletlenVacsora.Web.Models;

namespace VeletlenVacsora.Web.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class IngredientController : ControllerBase {
		private readonly IVacsoraRepository _repository;

		//TODO Implement Post Method
		//TODO Implement Put Method
		//TODO Implement Delete Method


		public IngredientController(IVacsoraRepository repository) {
			_repository = repository;
		}

		[HttpGet]
		public async Task<ActionResult<ICollection<IngredientModel>>> GetIngredientsAsync(string type = "", string package = "") {
			try {
				var results = new List<IngredientModel>();
				var raw = await _repository.GetIngredientsByTypeAsync(type, package);
				foreach (var i in raw) {
					results.Add(new IngredientModel(i));
				}
				return Ok(results);

			} catch (Exception ex) {
				return StatusCode(StatusCodes.Status500InternalServerError, $"Server Failure: {ex.GetType().Name}\n{ex.Message}");
			}
		}

		[HttpGet("{ID}")]
		public async Task<ActionResult<IngredientModel>> GetIngredientByIDAsync(int ID) {
			try {
				var raw = await _repository.GetIngredientByIDAsync(ID);
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