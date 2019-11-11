using System.Collections.Generic;
using VeletlenVacsora.Data.Models;

namespace VeletlenVacsora.Web.Models {
	public class RecepieModel {
		public int ID { get; set; } = 0;
		public string Name { get; set; } = "";

		public string Category { get; set; } = "";
		public int Price { get { return CalculatePrice(); } }

		public ICollection<RecepieIngredientModel> Ingredients { get; set; } = new List<RecepieIngredientModel>();

		public RecepieModel() { }

		public RecepieModel(Recepie rec) {
			ID = rec.ID;
			Name = rec.Name;
			Category = rec.Category.Name;

			if (rec.Ingredients != null) {
				foreach (RecepieIngredient ri in rec.Ingredients) {
					Ingredients.Add(new RecepieIngredientModel(ri));
				}
			}
		}

		public int CalculatePrice() {
			int answ = 0;
			if (Ingredients.Count != 0) {
				foreach (var i in Ingredients) {
					answ += i.Price;
				}
			}
			return answ;
		}
	}
}
