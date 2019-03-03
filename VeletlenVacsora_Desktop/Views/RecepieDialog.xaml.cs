using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace VeletlenVacsora.Desktop.Views {
	/// <summary>
	/// Interaction logic for RecepieDialog.xaml
	/// </summary>
	public partial class RecepieDialog : Window{

        public RecepieDialog() {
            InitializeComponent();
        }

        private void btnCancel_click(object sender, RoutedEventArgs e) {
            DialogResult = false;
			Close();
        }

        private void btnOk_click(object sender, RoutedEventArgs e) {
            DialogResult = true;
			Close();
        }
	}
}
