using System.ComponentModel.DataAnnotations;
using VeletlenVacsora.Data.Models;

namespace VeletlenVacsora.Web.Models {
	public class IngredientModel {

		public int ID { get; set; } = 0;
		[Required]
		public string Name { get; set; } = "";

		[Required]
		public string Type { get; set; } = "";

		[Required]
		public string Package { get; set; } = "";

		[Required]
		public int Price { get; set; } = 0;

		public IngredientModel() { }

		public IngredientModel(Ingredient i) {
			ID = i.ID;
			Name = i.Name;
			Type = i.IngredientType.Name;
			Package = i.PackageType.Name;
			Price = i.Price;
		}
	}
}
