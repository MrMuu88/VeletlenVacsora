using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using VacsoraDataModel;
using Xamarin.Forms;

namespace VeletlenVacsora.ViewModels {
	class Menu_VM : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;

		private VacsoraDBContext DB;

		private bool _IsLoading;
		public bool IsLoading {
			get { return _IsLoading; }
			set { _IsLoading = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsLoading")); }
		}

		public ICommand cmdRandomizeAll { get; set; }
		public ICommand cmdRandomize { get; set; }
		public ICommand cmdPin { get; set; }
		

		private ObservableCollection<Recepie> _Foods;
		public ObservableCollection<Recepie> Foods {
			get { return _Foods; }
			set { _Foods = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Foods")); }
		}

		public Menu_VM() {
			Foods = new ObservableCollection<Recepie>();
			DB = new VacsoraDBContext(App.ConnString, App.DBType);
			cmdRandomizeAll = new Command(RandomizeAllAsync);
			cmdRandomize = new Command(RandomizeAsync);
			cmdPin = new Command(Pin);
			Foods.CollectionChanged += (o, e) => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Foods")); };
		}


		private void Pin(object obj) {
			var f = (Recepie)obj;
			if (f.OnMenu) {
				f.OnMenu = false;
				DB.SaveChanges();
			} else {
				f.OnMenu = true;
				if (f.Weight > 10) { f.Weight -= 10; }
				DB.SaveChanges();
			}
		}

		private void RandomizeAsync(object obj) {
			var f = (Recepie)obj;
			var i = Foods.IndexOf(f);
			var Query = DB.Foods.FromSql("SELECT ID, Name, Weight, Price,OnMenu, Weight*Rand() as Chance FROM VacsoraDB.Foods order by chance DESC LIMIT 1").Include("Ingredients");
			Foods[i] = Query.FirstOrDefault();

		}

		private async void RandomizeAllAsync(object obj) {
			await Task.Run(() => {
				IsLoading = true;
				Debug.WriteLine("Randomize Menu started");
				var query = DB.Foods.FromSql("SELECT ID, Name, Weight, Price,OnMenu, Weight*Rand() as Chance FROM VacsoraDB.Foods order by chance DESC LIMIT 7").Include("Ingredients");
				Foods = new ObservableCollection<Recepie>(query.ToList());
				IsLoading = false;
			});
			return;
		}

		public async Task Refresh() {
			await Task.Run(() => {
				Foods = new ObservableCollection<Recepie>(DB.Foods.Where(f => f.OnMenu == true).Include("Ingredients").ToList());
			});
		}

	}
}
