using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VeletlenVacsora.Data {
	public class Recepie : INotifyPropertyChanged,ICloneable{
		private static Random Dice = new Random();

		public event PropertyChangedEventHandler PropertyChanged;

		private string _Name;
		private int _Chance;
		private ObservableCollection<RecepieIngredient> _Ingredients;
		//DELETEABLE ?
		private bool _OnMenu;

		[Key]
		public int RecepieID { get; set; }

		[MaxLength(50)]
		public string Name {
			get { return _Name; }
			set { _Name = value; RaisePropertyChanged(nameof(Name)); }
		}

		public int Chance {
			get { return _Chance; }
			set { _Chance = value; RaisePropertyChanged(nameof(Chance)); }
		}


		public double Weight {
			get { return (double)Chance * Dice.NextDouble(); }
		}



		public ObservableCollection<RecepieIngredient> Ingredients {
			get { return _Ingredients; }
			set { _Ingredients = value; RaisePropertyChanged(nameof(Ingredients)); }
		}

		public int Price {
			get { return CalcPrice(); }
			private set { }
		}


		public bool OnMenu {
			get { return _OnMenu; }
			set { _OnMenu = value; RaisePropertyChanged(nameof(OnMenu)); }
		}

		public Recepie() {
			Name = "";
			Chance = 90;
			Price = 0;
			Ingredients = new ObservableCollection<RecepieIngredient>();
		}

		public Recepie(string name = null, int chance = 90) {
			Name = name;
			Chance = chance;
			Price = 0;
			Ingredients = new ObservableCollection<RecepieIngredient>();
		}

		

        public object Clone() {
            return new Recepie() {
                Name = this.Name,
                Chance = this.Chance,
                Price = this.Price,
                OnMenu = this.OnMenu,
                Ingredients = this.Ingredients
            };
        }

		private int CalcPrice() {
			int Answ = 0;
			foreach (var ri in Ingredients) {
				Answ +=ri.Price;
			}
			return Answ;
		}

		private void RaisePropertyChanged(string PropertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
			
		}
	}//clss
}//ns
