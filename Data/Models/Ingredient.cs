using System.Collections.Generic;

namespace VeletlenVacsora.Data.Models {
	public class Ingredient :BaseModel{

		public Category IngredientType { get; set; }

		public Category PackageType { get; set; }

		public int Price { get; set; }

		public ICollection<RecepieIngredient> Recepies { get; set; }

		public Ingredient(string name):base(name) { }
		public Ingredient() { }

	}//clss

}//ns
