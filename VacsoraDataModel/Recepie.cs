using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VeletlenVacsora.Data;

namespace VeletlenVacsora.Data {
	public class Recepie : INotifyPropertyChanged,ICloneable {
		public event PropertyChangedEventHandler PropertyChanged;
		

		[Key]
		public int RecepieID { get; set; }

		private string _Name;
		[MaxLength(50)]
		public string Name {
			get { return _Name; }
			set { _Name = value; RaisePropertyChanged(nameof(Name)); }
		}

		private int _Weight;
		public int Weight {
			get { return _Weight; }
			set { _Weight = value; RaisePropertyChanged(nameof(Weight)); }
		}


		private ObservableCollection<RecepieIngredient> _Ingredients;
		public ObservableCollection<RecepieIngredient> Ingredients {
			get { return _Ingredients; }
			set { _Ingredients = value; RaisePropertyChanged(nameof(Ingredients)); }
		}

		public int Price {
			get { return CalcPrice(); }
			private set { }
		}


		private bool _OnMenu;
		public bool OnMenu {
			get { return _OnMenu; }
			set { _OnMenu = value; RaisePropertyChanged(nameof(OnMenu)); }
		}

		public Recepie() {
			Ingredients = new ObservableCollection<RecepieIngredient>();
		}

		public Recepie(string name = null, int weight = 90) {
			Name = name;
			Weight = weight;
			Price = 0;
			Ingredients = new ObservableCollection<RecepieIngredient>();
		}

		

        public object Clone() {
            return new Recepie() {
                Name = this.Name,
                Weight = this.Weight,
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
