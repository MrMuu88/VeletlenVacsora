using System.ComponentModel;
using System.Windows;
using VeletlenVacsora.Data;

namespace VeletlenVacsora.Desktop.Views {
    public partial class IngredientDialog : Window, INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;


        private Ingredient _Ingredient;
        public Ingredient Ingredient {
            get { return _Ingredient; }
            set { _Ingredient = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Ingredient))); }
        }
        public IngredientDialog() {
            InitializeComponent();
        }

        private void txtPrice_TextEnter(object sender, System.Windows.Input.TextCompositionEventArgs e) {
            e.Handled =!int.TryParse(e.Text, out var asd);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e) {
            DialogResult = true;
            Close();
        }
    }
}
