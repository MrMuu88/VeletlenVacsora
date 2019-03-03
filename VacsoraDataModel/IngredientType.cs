namespace VeletlenVacsora.Data {
	public class IngredientType {
		public virtual int ID{ get; set; }
		public virtual string TypeName{ get; set; }

		public IngredientType() {}

		public IngredientType(string name) {
			TypeName = name;
		}
	}
}
