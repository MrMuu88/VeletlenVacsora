using System.Collections.Generic;

namespace VeletlenVacsora.Data.Models {
	public class RecepieModel:BaseModel {

		public CategoryModel Category { get; set; }

		public ICollection<RecepieIngredientModel> Ingredients { get; set; }


		public RecepieModel(string name):base(name) { }
		public RecepieModel() { }

	}//clss
}//ns
