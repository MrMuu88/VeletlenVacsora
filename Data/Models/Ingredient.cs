using System.Collections.Generic;

namespace VeletlenVacsora.Data.Models {
	public class Ingredient {
		public int ID { get; set; }
		public string Name { get; set; }

		public Category IngredientType { get; set; }

		public Category PackageType { get; set; }

		public int Price { get; set; }

		public ICollection<RecepieIngredient> Recepies { get; set; }

		public Ingredient() { }

	}//clss

}//ns
