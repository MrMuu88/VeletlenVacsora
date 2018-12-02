using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VeletlenVacsora.Views {
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainNavigation : MasterDetailPage,INotifyPropertyChanged{
		public new event PropertyChangedEventHandler PropertyChanged;

		private ObservableCollection<MenuItem> _MenuItems;
		public ObservableCollection <MenuItem> MenuItems {
			get { return _MenuItems; }
			set { _MenuItems = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MenuItems")); }
		}

		public MainNavigation(){
			InitializeComponent();
			MenuItems = new ObservableCollection<MenuItem>();
			MenuItems.Add(new MenuItem("Heti Menü",typeof(MenuPage)));
			MenuItems.Add(new MenuItem("Receptek",typeof(RecepieManagerPage)));
			MenuItems.Add(new MenuItem("Hozzávalók",typeof(IngredientManagerPage)));
			MenuItems.Add(new MenuItem("Bevásárló lista",typeof(ShoppingListPage)));
		}

		private async void OnListView_ItemSelected(object sender, SelectedItemChangedEventArgs e) {
			var Selected = (MenuItem)e.SelectedItem;
			var NewPage = (Page)Activator.CreateInstance(Selected.PageType);
			Detail = new NavigationPage(NewPage);
			await Task.Delay(225);
			IsPresented = false;
		}

		public class MenuItem {
			public string Title { get; set; }
			public string Icon { get; set; }
			public Color BGColor { get; set; }
			public Type PageType { get; set; }

			public MenuItem(string title,Type type, string icon = "Icon.png", Color bgColor= default(Color)) {
				Title = title;
				Icon = icon;
				BGColor = bgColor;
				PageType = type;
			}
		}// Inner clss

	}//clss
}//ns