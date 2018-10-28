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
	class MainPage_VM:INotifyPropertyChanged{
		public event PropertyChangedEventHandler PropertyChanged;

		private VacsoraDBContext DB;

		private bool _IsDownLoading;
		public bool IsDownLoading {
			get { return _IsDownLoading; }
			set { _IsDownLoading = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsDownLoading")); }
		}

		public ICommand cmdRandomizeAll { get; set; }
		public ICommand cmdRandomize { get; set; }
		public ICommand cmdLike { get; set; }
		public ICommand cmdLikeAll { get; set; }

		private ObservableCollection<Food> _Foods;
		public ObservableCollection<Food> Foods {
			get { return _Foods; }
			set { _Foods = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Foods")); }
		}

		public MainPage_VM() {
			Foods = new ObservableCollection<Food>();
			DB = new VacsoraDBContext("Server = simbir.asuscomm.com; UID = Szakacs; PWD = MitFozzunk; database = VacsoraDB; Port = 3306", DBType.MySql);
			cmdRandomizeAll = new Command(RandomizeAllAsync);
			cmdRandomize = new Command(RandomizeAsync);
			cmdLike = new Command(Like);
			cmdLikeAll = new Command(LikeAll);

			Foods.CollectionChanged += (o, e)=>{PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Foods"));};
		}

		private void LikeAll(object obj) {
			foreach (var f in Foods) {
				if (f.Weight > 10) { f.Weight -= 10; }
			}
		}

		private void Like(object obj) {
			var f = (Food)obj;
			if (f.Weight > 10) { f.Weight -= 10; }
			DB.SaveChanges();
		}

		private void RandomizeAsync(object obj) {
			var f = (Food)obj;
			var i = Foods.IndexOf(f);
			var Query = DB.Foods.FromSql("SELECT ID, Name, Weight, Price, Weight*Rand() as Chance FROM VacsoraDB.Foods order by chance DESC LIMIT 1").Include("Ingredients");
			Foods[i] = Query.FirstOrDefault();

		}

		private async void RandomizeAllAsync(object obj) {
			await Task.Run(() => {
				IsDownLoading = true;
				Debug.WriteLine("Randomize Menu started");
				var query = DB.Foods.FromSql("SELECT ID, Name, Weight, Price, Weight*Rand() as Chance FROM VacsoraDB.Foods order by chance DESC LIMIT 7").Include("Ingredients").ToList();
				Foods = new ObservableCollection<Food>(query);
				IsDownLoading = false;
			});
			return;
		}
	}
}
