using System.Collections.Generic;
using VeletlenVacsora.Data;

namespace VeletlenVacsora.Services {
	public interface IVacsoraRepository {

		ICollection<Recepie> GetAllRecepies();
		Recepie GetRecepieByID(int id);

		ICollection<Category> GetAllCategories();
		ICollection<Category> GetCategoryByType(CategoryType type);
		Category GetCategoryByID(int ID);


		ICollection<Ingredient> GetAllIngredients();
		ICollection<Ingredient> GetIngredientsByRecepie(int recepieID);
		ICollection<Ingredient> GetIngredientsByType(string type, string package);

	}
}
