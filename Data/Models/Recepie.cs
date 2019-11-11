using System.Collections.Generic;

namespace VeletlenVacsora.Data.Models {
	public class Recepie {

		public int ID { get; set; }

		public string Name { get; set; }

		public Category Category { get; set; }

		public ICollection<RecepieIngredient> Ingredients { get; set; }


		public Recepie() { }

	}//clss
}//ns
