using VeletlenVacsora.Data;

namespace VeletlenVacsora.Web.Models {
	public class CategoryModel {

		public virtual int ID { get; set; } = 0;
		public string Name { get; set; } = "";
		public string Type { get; set; } = "";

		public CategoryModel() { }

		public CategoryModel(Category category) {
			ID = category.ID;
			Name = category.Name;
			Type = category.Type.ToString();
		}
	}
}