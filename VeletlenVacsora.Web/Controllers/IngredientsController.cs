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
				
				var type = await EnsureCategoryExists(model.Type, CategoryType.Ingredient);
				var package = await EnsureCategoryExists(model.Package, CategoryType.Package);
				
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

		[HttpDelete("{ID}")]
		public async Task<ActionResult> DeleteIngredientByID(int ID) {
			try {
				var toDelete = await _repository.GetIngredientByIDAsync(ID);
				if (toDelete != null) {
					_repository.Delete(toDelete);
					await _repository.SaveChangesAsync();

					return StatusCode(StatusCodes.Status200OK, $"Ingredient ID={ID} succesfully Deleted");

				} else {
					return StatusCode(StatusCodes.Status404NotFound, $"Ingredient with ID={ID} does Not Exists");
				}
			} catch (Exception ex) {

				return StatusCode(StatusCodes.Status500InternalServerError, $"Server Failure: {ex.GetType().Name}\n{ex.Message}");
			}
		}

		[HttpPut("{ID}")]
		public async Task<ActionResult<IngredientModel>> UpdateIngredientAsync(int ID ,IngredientModel model) {
			try {
				if (!ModelState.IsValid) {
					return BadRequest();
				}

				var ingredient = await _repository.GetIngredientByIDAsync(ID);

				var type = await EnsureCategoryExists(model.Type, CategoryType.Ingredient);
				var package = await EnsureCategoryExists(model.Package, CategoryType.Package);

				ingredient.IngredientType = type;
				ingredient.PackageType = package;
				ingredient.Name = model.Name;
				ingredient.Price = model.Price;

				_repository.Update(ingredient);
				if (await _repository.SaveChangesAsync()) {
					return StatusCode(StatusCodes.Status200OK, model);
				}
			} catch (Exception ex) {

				return StatusCode(StatusCodes.Status500InternalServerError, $"Server Failure: {ex.GetType().Name}\n{ex.Message}");
			}
			return BadRequest();
		}

		private async Task<Category> EnsureCategoryExists(string categoryName,CategoryType categoryType) {
			Category Answer = null;
			var type = await _repository.GetCategoryByNameAsync(categoryName);
			if (type == null) {
				Answer = new Category { Name = categoryName, Type = categoryType };
				_repository.Add(Answer);
			}
			return Answer;
		}
	}
}