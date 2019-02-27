using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Veletlenvacsora.Data;

namespace VeletlenVacsora.Data {
	public class Ingredient:INotifyPropertyChanged,ICloneable{
		public event PropertyChangedEventHandler PropertyChanged;

		[Key]
        public int IngredientID { get; set; }

		private string _Name;
		[MaxLength(50)]
		public string Name {
			get { return _Name; }
			set { _Name = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name")); }}


        private string _IngredientType;
        public string IngredientType {
            get { return _IngredientType; }
            set { _IngredientType = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IngredientType))); }
        }

        private string _PackageType;
		public  string PackageType {
			get { return _PackageType; }
			set { _PackageType = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PackageType))); }}

		private int _Price;
		public  int Price {
			get { return _Price; }
			set { _Price = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Price")); }
		}


        private ObservableCollection<RecepieIngredient> _Recepies;
        public ObservableCollection<RecepieIngredient> Recepies {
            get { return _Recepies; }
            set { _Recepies = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Recepies))); }
        }


        public Ingredient() {
            Name = "Tej";
            IngredientType = "Tejtermék";
            PackageType = "Liter";
            Price = 350;
        }

        public object Clone() {
            return new Ingredient() {
                Name = this.Name,
                IngredientType = this.IngredientType,
                PackageType = this.PackageType,
                Price = this.Price

            };
        }
    }//clss

	public enum PackageType {db,Doboz,Kg,üveg,Pohár,L}
}//ns
