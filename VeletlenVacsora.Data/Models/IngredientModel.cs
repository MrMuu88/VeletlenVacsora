using System.Collections.Generic;

namespace VeletlenVacsora.Data.Models {
	public class IngredientModel :BaseModel{

		public CategoryModel IngredientType { get; set; } = new CategoryModel(){Type = CategoryType.Ingredient};

		public CategoryModel PackageType { get; set; } = new CategoryModel() { Type = CategoryType.Package};

		public int Price { get; set; }

		public ICollection<RecepieIngredientModel> Recepies { get; set; } = new List<RecepieIngredientModel>();

		public IngredientModel(string name):base(name) { }
		public IngredientModel() { }

	}//clss

}//ns
