using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Veletlenvacsora.Data;

namespace VeletlenVacsora.Data {
	public class Recepie : INotifyPropertyChanged,ICloneable {
		public event PropertyChangedEventHandler PropertyChanged;

		[Key]
		public int RecepieID { get; set; }

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


		private ObservableCollection<RecepieIngredient> _RecepieIngredients;
		public ObservableCollection<RecepieIngredient> RecepieIngredients {
			get { return _RecepieIngredients; }
			set { _RecepieIngredients = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Ingredients")); }
		}

		private int _Price;
		public int Price {
			get { return _Price; }
			private set { _Price = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Price")); }
		}


		private bool _OnMenu;
		public bool OnMenu {
			get { return _OnMenu; }
			set { _OnMenu = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OnMenu")); }
		}

		

		public Recepie(string name = null, int weight = 90) {
			Name = name;
			Weight = weight;
			Price = 0;
			RecepieIngredients = new ObservableCollection<RecepieIngredient>();
		}

		

        public object Clone() {
            return new Recepie() {
                Name = this.Name,
                Weight = this.Weight,
                Price = this.Price,
                OnMenu = this.OnMenu,
                RecepieIngredients = this.RecepieIngredients
            };
        }
    }//clss
}//ns
