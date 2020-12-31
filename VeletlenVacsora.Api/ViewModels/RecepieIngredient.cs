namespace VeletlenVacsora.Api.ViewModels
{
	public class RecepieIngredient
	{

		public int RecepieID { get; set; }

		public int IngredientID { get; set; }

		public Recepie Recepie { get; set; }

		public Ingredient Ingredient { get; set; }

		public double Amount { get; set; }

		public RecepieIngredient()
		{

		}
	}
}
