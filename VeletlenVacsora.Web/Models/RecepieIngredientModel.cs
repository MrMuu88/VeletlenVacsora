using VeletlenVacsora.Data.Models;

namespace VeletlenVacsora.Api.Models {
	public class RecepieIngredientModel {
		public string Name { get; set; } = "";
		public string Type { get; set; } = "";
		public string Package { get; set; } = "";
		public double Amount { get; set; } = 0.0d;
		public int Price { get; set; } = 0;

		public RecepieIngredientModel() { }

		public RecepieIngredientModel(RecepieIngredient ri) {
			Name = ri.Ingredient.Name;
			Type = ri.Ingredient.IngredientType.Name;
			Package = ri.Ingredient.PackageType.Name;
			Amount = ri.Amount;
			Price = (int)(ri.Ingredient.Price * Amount);
		}
	}
}