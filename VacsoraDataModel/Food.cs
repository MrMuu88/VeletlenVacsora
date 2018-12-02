using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VacsoraDataModel {
	public class Food : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;

		[Key]
		public int ID { get; set; }

		private string _Name;
		[MaxLength(50)]
		public string Name {
			get { return _Name; }
			set { _Name = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name")); }
		}

		private int _Weight;
		public int Weight {
			get { return _Weight; }
			set { _Weight = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Weight")); }
		}


		private ObservableCollection<Ingredient> _Ingredients;
		public ObservableCollection<Ingredient> Ingredients {
			get { return _Ingredients; }
			set { _Ingredients = value; ReCalcPrice(); PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Ingredients")); }
		}

		private int _Price;
		public int Price {
			get { return _Price; }
			private set { _Price = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Price")); }
		}

		private void ReCalcPrice() {
			int newPrice = 0;
			foreach (var i in Ingredients) {
				newPrice += i.Price;
			}
			Price = newPrice;
		}

		public Food(string name = null, int weight = 90) {
			Name = name;
			Weight = weight;
			Price = 0;
			Ingredients = new ObservableCollection<Ingredient>();
		}

		public void CalcPrice() {
			int newPrice = 0;
			foreach (Ingredient i in Ingredients) {
				newPrice += i.Price;
			}
			Price = newPrice;
		}
	}//clss
}//ns
