namespace VeletlenVacsora.Data.Models {
	public class RecepieIngredientModel {

		public int RecepieId { get; set; }

		public int IngredientId { get; set; }

		public RecepieModel Recepie { get; set; }

		public IngredientModel Ingredient { get; set; }

		public double Amount { get; set; }


		public RecepieIngredientModel() {

		}
	}
}
