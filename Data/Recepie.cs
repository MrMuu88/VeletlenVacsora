using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VeletlenVacsora.Data {
	public class Recepie {

		[Key]
		public int ID { get; set; }

		[MaxLength(50)]
		public string Name { get; set; }


		public ICollection<RecepieIngredient> Ingredients { get; set; }


		public Recepie() { }

	}//clss
}//ns
