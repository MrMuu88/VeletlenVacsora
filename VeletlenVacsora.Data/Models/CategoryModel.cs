namespace VeletlenVacsora.Data.Models {
	public class CategoryModel :BaseModel{
		public CategoryType Type { get; set; }

		public CategoryModel(string name) : base(name) { }
		public CategoryModel() { }

	}

	
}
