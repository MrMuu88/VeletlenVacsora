using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using VeletlenVacsora.Data;

namespace VeletlenVacsora.Desktop.ViewModels {
	class RecepieDialogVM : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;


		private Recepie _Recepie;
		public Recepie Recepie {
			get { return _Recepie; }
			set { _Recepie = value; RaisePropertyChanged(nameof(Recepie)); }
		}


		private ObservableCollection<Ingredient> _Ingredients;
		public ObservableCollection<Ingredient> Ingredients {
			get { return _Ingredients; }
			set { _Ingredients = value; RaisePropertyChanged(nameof(Ingredients)); }
		}

		public ICommand cmdRemoveIngredient { get; private set; }
		public ICommand cmdAddIngredient { get; private set; }

		public RecepieDialogVM() {
			cmdRemoveIngredient = new RelayCommand<RecepieIngredient>(RemoveIngredient);
			cmdAddIngredient = new RelayCommand<Ingredient>(AddIngredient);
		}

		private void AddIngredient(Ingredient obj) {
			Recepie.Ingredients.Add(new RecepieIngredient(Recepie,obj));
		}

		private void RemoveIngredient(RecepieIngredient obj) {
			Debug.WriteLine(obj.Ingredient.Name);
			Recepie.Ingredients.Remove(obj);
		}

		private void RaisePropertyChanged(string PropertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		}
	}
}
