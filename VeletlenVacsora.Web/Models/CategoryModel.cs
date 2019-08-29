using VeletlenVacsora.Data;

namespace VeletlenVacsora.Web.Models {
	public class CategoryModel {

		public virtual int ID { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }

		public CategoryModel(Category category) {
			ID = category.ID;
			Name = category.Name;
			Type = category.Type.ToString();
		}
	}
}