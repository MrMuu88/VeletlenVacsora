using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using VeletlenVacsora.Data;
using VeletlenVacsora.Desktop.Views;

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
		public ICommand cmdEditIngredient { get; private set; }

		public RecepieDialogVM() {
			cmdRemoveIngredient = new RelayCommand<RecepieIngredient>(RemoveIngredient);
			cmdAddIngredient = new RelayCommand<Ingredient>(AddIngredient);
			cmdEditIngredient = new RelayCommand<Ingredient>(EditIngredient);
		}

		private void AddIngredient(Ingredient obj) {
			Recepie.Ingredients.Add(new RecepieIngredient(Recepie,obj));
		}

		private void RemoveIngredient(RecepieIngredient obj) {
			Debug.WriteLine(obj.Ingredient.Name);
			Recepie.Ingredients.Remove(obj);
		}

		private void EditIngredient(Ingredient selected) {

			if (selected == null) { selected = new Ingredient(); }
			Debug.WriteLine($"Editing Ingredient '{selected.Name}'");
			var unmodified = selected.Clone();
			var IngDialog = new IngredientDialog();

			var DDContext = (IngredientDialogVM)IngDialog.DataContext;
			DDContext.Ingredient = selected;

			bool result = (bool)IngDialog.ShowDialog();
			if (result) {
				App.DB.Ingredients.Update(DDContext.Ingredient);
				if (!App.SaveToDB()) {
					//TODO save failed undo modifications
				}
				Ingredients = App.DB.Ingredients.Local.ToObservableCollection();
			}
		}

		private void RaisePropertyChanged(string PropertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		}
	}
}
