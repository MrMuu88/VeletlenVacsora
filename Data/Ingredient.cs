using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VeletlenVacsora.Data {
	public class Ingredient:INotifyPropertyChanged,ICloneable{
		public event PropertyChangedEventHandler PropertyChanged;
		public event EventHandler PriceChanged;

		private string _Name;
        private Category _IngredientType;
        private Category _PackageType;
		private int _Price;
        private ObservableCollection<RecepieIngredient> _Recepies;

		[Key]
        public int IngredientID { get; set; }

		[MaxLength(50)]
		public string Name {
			get { return _Name; }
			set { _Name = value; RaisePropertyChanged(nameof(Name)); }}


        public  Category IngredientType {
            get { return _IngredientType; }
            set { _IngredientType = value; RaisePropertyChanged(nameof(IngredientType)); }
        }

		public  Category PackageType {
			get { return _PackageType; }
			set { _PackageType = value; RaisePropertyChanged(nameof(PackageType)); }}

		public  int Price {
			get { return _Price; }
			set { _Price = value; RaisePropertyChanged(nameof(Price)); }
		}


        public ObservableCollection<RecepieIngredient> Recepies {
            get { return _Recepies; }
            set { _Recepies = value; RaisePropertyChanged(nameof(Recepies)); }
        }

        public Ingredient() {
            Name = "Tej";
            Price = 350;
        }

		public Ingredient(string name,Category ingType,Category packType,int price):this(){
			Name = name;
			IngredientType = ingType;
			PackageType = packType;
			Price = price;
		}

        public object Clone() {
            return new Ingredient() {
                Name = this.Name,
                IngredientType = this.IngredientType,
                PackageType = this.PackageType,
                Price = this.Price

            };
        }

		private void RaisePropertyChanged(string PropertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
			if (PropertyName == nameof(Price)) {
				PriceChanged?.Invoke(this, EventArgs.Empty);
			}
		}

	}//clss

}//ns
