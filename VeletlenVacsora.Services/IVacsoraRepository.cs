using System.Collections.Generic;
using VeletlenVacsora.Data;

namespace VeletlenVacsora.Services {
	public interface IVacsoraRepository {

		ICollection<Recepie> GetAllRecepies();
		ICollection<Recepie> GetRecepiesByType();
		Recepie GetRecepieByID(int id);


		ICollection<Category> GetAllCategories();
		ICollection<Category> GetCategoryByType(string type);
		Category GetCategoryByID(int ID);


		ICollection<Ingredient> GetAllIngredients();
		ICollection<Ingredient> GetIngredientsByType(string type, string package);
		Ingredient GetIngredientByID(int ID);

	}
}
