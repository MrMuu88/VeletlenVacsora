using System.Collections.ObjectModel;
using System.ComponentModel;
using VeletlenVacsora.Data;

namespace VeletlenVacsora.Desktop.ViewModels {
	public class IngredientDialogVM : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;

		//public Ingredient Ingredient { get; set; }


		private Ingredient _Ingredient;
		public Ingredient Ingredient {
			get { return _Ingredient; }
			set { _Ingredient = value; RaisePropertyChanged(nameof(Ingredient)); }
		}

		private ObservableCollection<IngredientType> _IngredientTypes;
		public ObservableCollection<IngredientType> IngredientTypes {
			get { return _IngredientTypes; }
			set { _IngredientTypes = value; RaisePropertyChanged(nameof(IngredientTypes)); }
		}

		private ObservableCollection<PackageType> _PackageTypes;
		public ObservableCollection <PackageType>  PackageTypes {
			get { return _PackageTypes; }
			set { _PackageTypes = value; RaisePropertyChanged(nameof(PackageTypes)); }
		}


		private void RaisePropertyChanged(string PropertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		}
	}
}
