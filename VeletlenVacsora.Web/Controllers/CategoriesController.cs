using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using VeletlenVacsora.Data;
using VeletlenVacsora.Services;
using VeletlenVacsora.Web.Models;

namespace VeletlenVacsora.Web.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase {
		private IVacsoraRepository _repository;

		public CategoriesController(IVacsoraRepository repository) {
			_repository = repository;
		}

		[HttpGet]
		public ActionResult<ICollection<CategoryModel>> GetCategories(string type = "") {
			try {
				var results = new List<CategoryModel>();
				List<Category> raw;
				if (!string.IsNullOrWhiteSpace(type)) {
					raw = (List<Category>)_repository.GetCategoryByType(type);
				} else {
					raw = (List<Category>)_repository.GetAllCategories();
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
		public ActionResult<CategoryModel> GetCategoryByID(int ID) {
			try {
				var c = _repository.GetCategoryByID(ID);
				if (c != null) {
					return new CategoryModel(c);
				} else {
					return NotFound();
				}

			} catch (Exception ex) {
				return StatusCode(StatusCodes.Status500InternalServerError, $"Server Failure: {ex.GetType().Name}\n{ex.Message}");
			}
		}



	}
}