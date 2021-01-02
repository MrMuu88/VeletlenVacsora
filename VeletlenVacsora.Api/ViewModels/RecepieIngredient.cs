namespace VeletlenVacsora.Api.ViewModels
{
	public class RecepieIngredient
	{

		public int IngredientID { get; set; }

		public string Ingredient { get; set; }
		public string PackageType { get; set; }
		public string IngredientType { get; set; }
		public double Amount { get; set; }
		public bool IsOnShopingList { get; set; }

		public RecepieIngredient()
		{

		}
	}
}
