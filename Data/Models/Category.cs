namespace VeletlenVacsora.Data.Models {
	public class Category :BaseModel{
		public CategoryType Type { get; set; }

		public Category(string name) : base(name) { }
		public Category() { }

	}

	public enum CategoryType { Ingredient, Package, Recepie }
}
