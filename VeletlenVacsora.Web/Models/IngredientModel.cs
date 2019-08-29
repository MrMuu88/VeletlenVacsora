using VeletlenVacsora.Data;

namespace VeletlenVacsora.Web.Models {
	public class IngredientModel {
		public string Name { get; set; }
		public string IngredientType { get; set; }
		public string PackageType { get; set; }
		public double Amount { get; set; }
		public int Price { get; set; }

		public IngredientModel(RecepieIngredient ri) {
			Name = ri.Ingredient.Name;
			IngredientType = ri.Ingredient.IngredientType.Name;
			PackageType = ri.Ingredient.PackageType.Name;
			Amount = ri.Amount;
			Price = (int)(ri.Ingredient.Price * Amount);
		}
	}
}