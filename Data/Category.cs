namespace VeletlenVacsora.Data {
	public class Category {

		public virtual int ID { get; set; }
		public string Name { get; set; }
		public CategoryType Type { get; set; }

		public Category() { }

	}

	public enum CategoryType { Ingredient, Package, Recepie }
}
