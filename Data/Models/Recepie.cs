using System.Collections.Generic;

namespace VeletlenVacsora.Data.Models {
	public class Recepie:BaseModel {

		public Category Category { get; set; }

		public ICollection<RecepieIngredient> Ingredients { get; set; }


		public Recepie(string name):base(name) { }

	}//clss
}//ns
