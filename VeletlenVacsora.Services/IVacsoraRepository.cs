using System.Collections.Generic;
using System.Threading.Tasks;
using VeletlenVacsora.Data;

namespace VeletlenVacsora.Services {
	public interface IVacsoraRepository {

		//TODO Modify Repository Interface Sygnatures to be async tasks

		Task<ICollection<Recepie>> GetAllRecepiesAsync();
		Task<ICollection<Recepie>> GetRecepiesByTypeAsync(string type);
		Task<Recepie> GetRecepieByIDAsync(int id);


		ICollection<Category> GetAllCategories();
		ICollection<Category> GetCategoryByType(string type);
		Category GetCategoryByID(int ID);


		ICollection<Ingredient> GetAllIngredients();
		ICollection<Ingredient> GetIngredientsByType(string type, string package);
		Ingredient GetIngredientByID(int ID);


		void Add<T>(T obj) where T : class;
		void Delete<T>(T obj) where T : class;
		Task<bool> SaveChangesAsync();
	}
}
