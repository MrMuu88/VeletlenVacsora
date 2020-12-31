namespace VeletlenVacsora.Data.Models {
	public class RecepieIngredientModel {

		public int RecepieID { get; set; }

		public int IngredientID { get; set; }

		public RecepieModel Recepie { get; set; }

		public IngredientModel Ingredient { get; set; }

		public double Amount { get; set; }


		public RecepieIngredientModel() {

		}
	}
}
