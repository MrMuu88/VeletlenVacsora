using VeletlenVacsora.Data;

namespace VeletlenVacsora.Web.Models {
	public class IngredientModel {
		public int ID { get; set; }
		public string Name { get; set; }

		public string Type { get; set; }

		public string Package { get; set; }

		public int Price { get; set; }

		public IngredientModel(Ingredient i) {
			ID = i.ID;
			Name = i.Name;
			Type = i.IngredientType.Name;
			Package = i.PackageType.Name;
			Price = i.Price;
		}
	}
}
