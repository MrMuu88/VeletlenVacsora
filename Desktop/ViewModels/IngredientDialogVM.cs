using GalaSoft.MvvmLight.Command;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using VeletlenVacsora.Data;
using VeletlenVacsora.Desktop.Views;

namespace VeletlenVacsora.Desktop.ViewModels {
	public class IngredientDialogVM : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;

		public ICommand cmdNewCategory{ get; private set; }
		

		private Ingredient _Ingredient;
		public Ingredient Ingredient {
			get { return _Ingredient; }
			set { _Ingredient = value; RaisePropertyChanged(nameof(Ingredient)); }
		}

		private ObservableCollection<Category> _IngredientTypes;
		public  ObservableCollection<Category> IngredientTypes {
			get { return _IngredientTypes; }
			set { _IngredientTypes = value; RaisePropertyChanged(nameof(IngredientTypes)); }
		}

		private ObservableCollection<Category> _PackageTypes;
		public  ObservableCollection<Category>  PackageTypes {
			get { return _PackageTypes; }
			set { _PackageTypes = value; RaisePropertyChanged(nameof(PackageTypes)); }
		}


		private void RaisePropertyChanged(string PropertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		}

		public IngredientDialogVM() {
			cmdNewCategory = new RelayCommand<string>(NewCategory);
			
			
			IngredientTypes = new ObservableCollection<Category>(App.DB.Categories.Local.Where(c => c.Type == CategoryType.Ingredient).ToArray());
			PackageTypes = new ObservableCollection<Category>(App.DB.Categories.Local.Where(c => c.Type == CategoryType.Package).ToArray());
			
		}

		private void NewCategory(string obj) {
			Debug.WriteLine($"creatring new {obj} Category");
			CategoryType createType = CategoryType.Ingredient;
			var CatDialog = new CategoryDialog();
			CatDialog.Category = new Category("",(CategoryType)Enum.Parse(typeof(CategoryType), obj));
			var result = CatDialog.ShowDialog();
			if ((bool)result) {
				App.DB.Categories.Update(CatDialog.Category);
				if(!App.SaveToDB()){ 
					//TODO Impelemt Logic when save fail
				}

				IngredientTypes = new ObservableCollection<Category>(App.DB.Categories.Local.Where(c => c.Type == CategoryType.Ingredient).ToArray());
				PackageTypes = new ObservableCollection<Category>(App.DB.Categories.Local.Where(c => c.Type == CategoryType.Package).ToArray());
			}
		}
	}
}
