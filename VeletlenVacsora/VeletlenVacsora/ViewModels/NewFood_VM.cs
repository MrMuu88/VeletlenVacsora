using System;
using System.ComponentModel;
using System.Windows.Input;
using VacsoraDataModel;
using VeletlenVacsora.Views;
using Xamarin.Forms;

namespace VeletlenVacsora.ViewModels {
	class NewFood_VM:INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;

		private Food _WorkingFood;
		public Food WorkingFood {
			get { return _WorkingFood; }
			set { _WorkingFood = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WorkingFood")); }
		}

		public ICommand cmdAddIngredient { get; set; }
		public ICommand cmdRemoveIngredient { get; set; }
		public ICommand cmdSave { get; set; }
		public ICommand cmdCancel { get; set; }

		public NewFood_VM() {
			WorkingFood = new Food();
			cmdAddIngredient = new Command(AddIngredient);
			cmdRemoveIngredient = new Command(RemoveIngredient);
			cmdSave = new Command(Save);
			cmdCancel = new Command(Cancel);
		}

		private async void Cancel(object obj) {
			App.Logger.MakeLog("Recepie modification canceled");
			await ((MainNavigation)Application.Current.MainPage).Navigation.PopModalAsync();
		}

		private async void Save(object obj) {
			using (var DB = new VacsoraDBContext(App.ConnString,App.DBType)) {
				try {
					WorkingFood.CalcPrice();
					DB.Foods.Update(WorkingFood);
					DB.SaveChanges();
					App.Logger.MakeLog("Changes are saved to DB");
					await ((MainNavigation)Application.Current.MainPage).Navigation.PopModalAsync();

				} catch(Exception Ex) {
					App.LogException(Ex);
				}
			}
		}

		private void RemoveIngredient(object obj) {
			if (obj != null) {
				var Selected = (Ingredient)obj;
				App.Logger.MakeLog($"Removing {Selected.Name} from {WorkingFood.Name}");
				WorkingFood.Ingredients.Remove((Selected));
				using (var DB = new VacsoraDBContext(App.ConnString, App.DBType)) {
					DB.Update(WorkingFood);
					DB.Remove(Selected);
					DB.SaveChanges();
				}
			}
		}

		private void AddIngredient(object obj) {
			App.Logger.MakeLog($"Adding new Ingredient to {WorkingFood.Name}");
			WorkingFood.Ingredients.Add(new Ingredient());
		}
	}
}
