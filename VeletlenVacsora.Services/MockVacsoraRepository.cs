using System.Collections.Generic;
using System.Linq;
using VeletlenVacsora.Data;

namespace VeletlenVacsora.Services {
	public class MockVacsoraRepository : IVacsoraRepository {
		private List<Category> _categories;
		private List<Recepie> _recepies;
		private List<Ingredient> _ingredients;
		public MockVacsoraRepository() {

			_categories = new List<Category> {
				new Category{ID=1,Type=CategoryType.Recepie,Name="Egytál" },
				new Category{ID=2,Type=CategoryType.Recepie,Name="Pörkölt" },
				new Category{ID=3,Type=CategoryType.Recepie,Name="Sült" },
				new Category{ID=4,Type=CategoryType.Ingredient,Name="Húsok" },
				new Category{ID=5,Type=CategoryType.Ingredient,Name="Zöldségek" },
				new Category{ID=6,Type=CategoryType.Ingredient,Name="Tésztaféle" },
				new Category{ID=7,Type=CategoryType.Ingredient,Name="Tejtermék" }
			};

			_recepies = new List<Recepie> {
				new Recepie() { ID = 1, Name = "Paprikás krumpli",Category= _categories[1] },
				new Recepie() { ID = 2, Name = "Túrós csusza",Category=_categories[0] },
				new Recepie() { ID = 3, Name = "Pizza", Category=_categories[2]},
				new Recepie() { ID = 4, Name = "Bolognai",Category=_categories[0] },
				new Recepie() { ID = 5, Name = "Chilis bab",Category=_categories[0] }
			};
		}

		public IEnumerable<Recepie> GetAllRecepies() {
			return _recepies;
		}

		public Recepie GetRecepieByID(int id) {
			return _recepies.Where(r => r.ID == id).FirstOrDefault();
		}
	}
}
