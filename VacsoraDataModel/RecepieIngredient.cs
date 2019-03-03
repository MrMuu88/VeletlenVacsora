using System;
using System.ComponentModel;
using System.Diagnostics;

namespace VeletlenVacsora.Data {
	public class RecepieIngredient:INotifyPropertyChanged {


		public int RecepieID { get; set; }
		public int IngredientID { get; set; }

		private Recepie _Recepie;

		public Recepie Recepie {
			get { return _Recepie; }
			set { _Recepie = value;
			RaisePropertyChanged(nameof(Recepie)); }
		}

		private Ingredient _Ingredient;

		public Ingredient Ingredient {
			get { return _Ingredient; }
			set { _Ingredient = value;
				RaisePropertyChanged(nameof(Ingredient));
			}
		}

		private double _Amount;

		public double Amount {
			get { return _Amount; }
			set { _Amount = value;
				RaisePropertyChanged(nameof(Amount));
				RaisePropertyChanged(nameof(Price));
			}
		}

		public int Price {
			get {
				return (int)Math.Ceiling( Amount * Ingredient.Price); }
			private set { } }

		public RecepieIngredient() {
			Amount = 0;
			Ingredient = new Ingredient();
			Recepie = new Recepie();
			Ingredient.PriceChanged += (s,e)=>{ RaisePropertyChanged(nameof(Price)); };
        }

        public RecepieIngredient(Recepie rec, Ingredient ing,double amount=1.0d) {
            Recepie = rec;
            Ingredient = ing;
			Amount = amount;
			Ingredient.PriceChanged += (s, e) => { RaisePropertyChanged(nameof(Price)); };
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChanged(string PropertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
			
		}
	}
}
