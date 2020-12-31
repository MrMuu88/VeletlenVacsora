using System.Collections.Generic;

namespace VeletlenVacsora.Api.ViewModels
{
	public class Ingredient
	{

		public Category IngredientType { get; set; }

		public Category PackageType { get; set; }

		public int Price { get; set; }

		public ICollection<RecepieIngredient> Recepies { get; set; }
		public Ingredient() { }

	}//clss

}//ns
