using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeletlenVacsora.Data;
using VeletlenVacsora.Services;
using VeletlenVacsora.Web.Models;

namespace VeletlenVacsora.Web.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class IngredientsController : ControllerBase {
		private readonly IVacsoraRepository _repository;

		//TODO Implement Put Method
		//TODO Implement Delete Method


		public IngredientsController(IVacsoraRepository repository) {
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

		[HttpPost]
		public async Task<ActionResult<IngredientModel>> PostNewIngredient(IngredientModel model) {

			if (!ModelState.IsValid) {
				return BadRequest();
			}
			try {
				//Check if Type category exist, create if not
				var type = await _repository.GetCategoryByNameAsync(model.Type);
				if (type == null) {
					_repository.Add(new Category { Name = model.Type, Type = CategoryType.Ingredient });
				}

				var package = await _repository.GetCategoryByNameAsync(model.Package);
				if (package == null) {
					_repository.Add(new Category { Name = model.Package, Type = CategoryType.Package });
				}

				var ingredient = new Ingredient() {
					Name = model.Name,
					Price = model.Price,
					IngredientType = type,
					PackageType = package,
				};

				_repository.Add(ingredient);

				if (await _repository.SaveChangesAsync()) {
					return Created("", model);
				} else {
					return BadRequest();
				}
			} catch (Exception ex) {
				return StatusCode(StatusCodes.Status500InternalServerError, $"Server Failure: {ex.GetType().Name}\n{ex.Message}");
			}
		}


	}
}