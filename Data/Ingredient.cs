using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VeletlenVacsora.Data {
	public class Ingredient {
		[Key]
		public int ID { get; set; }

		[MaxLength(50)]
		public string Name { get; set; }

		public Category IngredientType { get; set; }

		public Category PackageType { get; set; }

		public int Price { get; set; }

		public ICollection<RecepieIngredient> Recepies { get; set; }

		public Ingredient() { }

	}//clss

}//ns
