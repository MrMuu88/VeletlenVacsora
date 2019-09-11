using System.Collections.Generic;
using System.Threading.Tasks;
using VeletlenVacsora.Data;

namespace VeletlenVacsora.Services {
	public interface IVacsoraRepository {

		Task<ICollection<Recepie>> GetAllRecepiesAsync();
		Task<ICollection<Recepie>> GetRecepiesByTypeAsync(string type);
		Task<Recepie> GetRecepieByIDAsync(int id);


		Task<ICollection<Category>> GetAllCategoriesAsync();
		Task<ICollection<Category>> GetCategoryByTypeAsync(string type);
		Task<Category> GetCategoryByIDAsync(int ID);


		Task<ICollection<Ingredient>> GetAllIngredientsAsync();
		Task<ICollection<Ingredient>> GetIngredientsByTypeAsync(string type, string package);
		Task<Ingredient> GetIngredientByIDAsync(int ID);


		void Add<T>(T obj) where T : class;
		void Delete<T>(T obj) where T : class;
		Task<bool> SaveChangesAsync();
	}
}
