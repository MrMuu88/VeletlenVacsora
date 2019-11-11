using System.ComponentModel.DataAnnotations;
using VeletlenVacsora.Data.Models;

namespace VeletlenVacsora.Web.Models {
	public class CategoryModel {

		public int ID { get; set; } = 0;
		[Required]
		public string Name { get; set; } = "";
		[Required]
		public string Type { get; set; } = "";

		public CategoryModel() { }

		public CategoryModel(Category category) {
			ID = category.ID;
			Name = category.Name;
			Type = category.Type.ToString();
		}
	}
}