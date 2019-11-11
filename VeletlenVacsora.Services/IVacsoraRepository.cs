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
		Task<Category> GetCategoryByNameAsync(string Name);


		Task<ICollection<Ingredient>> GetAllIngredientsAsync();
		Task<ICollection<Ingredient>> GetIngredientsByTypeAsync(string type, string package);
		Task<Ingredient> GetIngredientByIDAsync(int ID);


		Task<bool> Add<T>(T obj) where T : class;
		Task<bool> AddRange<T>(IEnumerable<T> objs) where T : class;
		Task<bool> Delete<T>(T obj) where T : class;
		Task<bool> Update<T>(T obj) where T : class;

		
	}
}
