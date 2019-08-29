using Microsoft.AspNetCore.Mvc;
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
		public ICollection<CategoryModel> GetAll(CategoryType? type = null) {
			var results = new List<CategoryModel>();
			List<Category> raw;
			if (type != null) {
				raw = (List<Category>)_repository.GetCategoryByType((CategoryType)type);
			} else {
				raw = (List<Category>)_repository.GetAllCategories();
			}
			foreach (var c in raw) {
				results.Add(new CategoryModel(c));
			}

			return results;
		}

		[HttpGet("{ID}")]
		public CategoryModel GetCategoryByID(int ID) {
			var c = _repository.GetCategoryByID(ID);
			if (c != null) {
				return new CategoryModel(c);
			} else {
				return null;
			}
		}



	}
}