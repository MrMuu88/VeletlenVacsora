using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using VeletlenVacsora.Data;

namespace VeletlenVacsora.Desktop.Views {
	/// <summary>
	/// Interaction logic for Category_Dialog.xaml
	/// </summary>
	public partial class CategoryDialog : Window,INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;


		private Category _Category;
		public Category Category {
			get { return _Category; }
			set {
				_Category = value;
				RaisePropertyChanged(nameof(Category));
			}
		}

		public CategoryDialog() {
			InitializeComponent();
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e) {
			DialogResult = false;
		}

		private void btnOk_Click(object sender, RoutedEventArgs e) {
			Debug.WriteLine(Category.Name);
			DialogResult = true;
		}


		private void RaisePropertyChanged(string PropertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		}

	}
}
