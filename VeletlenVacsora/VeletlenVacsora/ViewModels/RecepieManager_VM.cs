using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using VacsoraDataModel;
using VeletlenVacsora.Views;
using Xamarin.Forms;

namespace VeletlenVacsora.ViewModels {
	class RecepieManager_VM:INotifyPropertyChanged{
		public event PropertyChangedEventHandler PropertyChanged;


		private bool _IsLoading;
		public bool IsLoading {
			get { return _IsLoading; }
			set { _IsLoading = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsLoading")); }
		}


		private string _LoadingMessage;
		public string LoadingMessage {
			get { return _LoadingMessage; }
			set { _LoadingMessage = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LoadingMessage")); }
		}

		public ICommand cmdEdit { get; set; }
		public ICommand cmdRemove { get; set; }

		private ObservableCollection<Recepie> _Foods;
		public ObservableCollection<Recepie> Foods {
			get { return _Foods; }
			set { _Foods = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Foods")); }
		}

		public RecepieManager_VM() {
			cmdEdit = new Command(Edit);
			cmdRemove = new Command(Remove);
		}

		private void Remove(object obj) {
			var Selected = (Recepie)obj;
			if (Selected != null) {
				try {
					using (var DB = new VacsoraDBContext(App.ConnString, App.DBType)) {
						DB.Foods.Remove(Selected);
						DB.SaveChanges();
						Refresh();
					}
				} catch (Exception Ex){
					App.LogException(Ex);
				}
			}
		}

		public async Task Refresh() {
			Foods = new ObservableCollection<Recepie>();
			IsLoading = true;
			LoadingMessage = "Adatok Letöltése";
			await Task.Run(() => {
				try{
					using (var DB = new VacsoraDBContext(App.ConnString, App.DBType)) {
						Foods = new ObservableCollection<Recepie>(DB.Foods.Include("Ingredients").ToList());
					}
				} catch (Exception Ex) {
					App.LogException(Ex);
				}			
			});
			IsLoading = false;
		}
		

		private async void Edit(object obj) {
			var fPage = new NewFoodPage();
			if (obj != null) {
				var Selected = (Recepie)obj;
				App.Logger.MakeLog($"Loading {Selected.Name}");
				((NewFood_VM)fPage.BindingContext).WorkingFood = Selected;
			}
			await ((MainNavigation)Application.Current.MainPage).Navigation.PushModalAsync(fPage);

		}
	}
}
