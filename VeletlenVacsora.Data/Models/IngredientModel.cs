using System.Collections.Generic;

namespace VeletlenVacsora.Data.Models {
	public class IngredientModel :BaseModel{

		public CategoryModel IngredientType { get; set; }

		public CategoryModel PackageType { get; set; }

		public int Price { get; set; }

		public ICollection<RecepieIngredientModel> Recepies { get; set; }

		public IngredientModel(string name):base(name) { }
		public IngredientModel() { }

	}//clss

}//ns
