using System.Collections.Generic;

namespace VeletlenVacsora.Data.Models {
	public class RecepieModel:BaseModel {
		public int Weight { get; set; }
		public CategoryModel Category { get; set; }
		public int? OnMenu { get; set; }

		public ICollection<RecepieIngredientModel> Ingredients { get; internal set; }


		public RecepieModel(string name):base(name) { }
		public RecepieModel() { }

	}//clss
}//ns
