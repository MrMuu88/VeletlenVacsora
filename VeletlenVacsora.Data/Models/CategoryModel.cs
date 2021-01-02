namespace VeletlenVacsora.Data.Models {
	public class CategoryModel :BaseModel{
		public CategoryType Type { get; set; }

		public CategoryModel(string name,CategoryType type = CategoryType.Ingredient) : base(name) {
			Type = type;
		}
		public CategoryModel() { }

	}

	
}
