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
	public class CategoriesController : ControllerBase {

		//TODO Implement Put Method
		//TODO Implement Delete Method

		private IVacsoraRepository _repository;

		public CategoriesController(IVacsoraRepository repository) {
			_repository = repository;
		}

		[HttpGet]
		public async Task<ActionResult<ICollection<CategoryModel>>> GetCategories(string type = "") {
			try {
				var results = new List<CategoryModel>();
				List<Category> raw;
				if (!string.IsNullOrWhiteSpace(type)) {
					raw = (List<Category>)await _repository.GetCategoryByTypeAsync(type);
				} else {
					raw = (List<Category>)await _repository.GetAllCategoriesAsync();
				}
				foreach (var c in raw) {
					results.Add(new CategoryModel(c));
				}

				return Ok(results);
			} catch (Exception ex) {
				return StatusCode(StatusCodes.Status500InternalServerError, $"Server Failure: {ex.GetType().Name}\n{ex.Message}");
			}



		}

		[HttpGet("{ID}")]
		public async Task<ActionResult<CategoryModel>> GetCategoryByID(int ID) {
			try {
				var c = await _repository.GetCategoryByIDAsync(ID);
				if (c != null) {
					return Ok(new CategoryModel(c));
				} else {
					return NotFound();
				}

			} catch (Exception ex) {
				return StatusCode(StatusCodes.Status500InternalServerError, $"Server Failure: {ex.GetType().Name}\n{ex.Message}");
			}
		}

		[HttpPost]
		public async Task<ActionResult<CategoryModel>> PostNew(CategoryModel model) {
			try {

				if (ModelState.IsValid) {

					CategoryType catType;
					if (Enum.TryParse(model.Type, true, out catType)) {
						var newcat = new Category { Name = model.Name, Type = catType };
						_repository.Add(newcat);
						await _repository.SaveChangesAsync();
						return Created("", new CategoryModel(newcat));
					}

				}

				return BadRequest();

			} catch (Exception ex) {
				return StatusCode(StatusCodes.Status500InternalServerError, $"Server Failure: {ex.GetType().Name}\n{ex.Message}");
			}
		}
	}
}