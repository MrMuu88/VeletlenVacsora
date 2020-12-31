using System.Collections.Generic;

namespace VeletlenVacsora.Api.ViewModels
{
	public class Ingredient
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public string IngredientType { get; set; }

		public string PackageType { get; set; }

		public int Price { get; set; }

		public Ingredient() { }

	}//clss

}//ns
