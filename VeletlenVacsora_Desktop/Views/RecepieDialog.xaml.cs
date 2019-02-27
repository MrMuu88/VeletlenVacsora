using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Veletlenvacsora.Data;
using VeletlenVacsora.Data;

namespace VeletlenVacsora.Desktop.Views {
    /// <summary>
    /// Interaction logic for RecepieDialog.xaml
    /// </summary>
    public partial class RecepieDialog : Window, INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;


        private Recepie _Recepie;
        public Recepie Recepie {
            get { return _Recepie; }
            set { _Recepie = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Recepie))); }
        }


        private ObservableCollection<Ingredient> _Ingredients;
        public ObservableCollection<Ingredient> Ingredients {
            get { return _Ingredients; }
            set { _Ingredients = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Ingredients))); }
        }

        public RecepieDialog() {
            InitializeComponent();
        }


        private void btnCancel_click(object sender, RoutedEventArgs e) {
            DialogResult = false;
        }

        private void btnOk_click(object sender, RoutedEventArgs e) {
            DialogResult = true;
        }

        private void btnAddIngredient(object sender, RoutedEventArgs e) {
            Debug.WriteLine(Ingredients.Count);
            Recepie.RecepieIngredients.Add(new RecepieIngredient(Recepie,new Ingredient()));
        }

        private void RemoveSelectedrecepie(object sender, MouseButtonEventArgs e) {
            //Recepie.RecepieIngredients.Remove((Ingredient)lstRecepieIngredients.SelectedItem);
        }

        private void AddSelected_recepie(object sender, MouseButtonEventArgs e) {
            Recepie.RecepieIngredients.Add(new RecepieIngredient(Recepie,(Ingredient)lstIngredients.SelectedItem));
        }

        private void mniNewIngredient(object sender, RoutedEventArgs e) {
            Debug.WriteLine("Creating new Ingredient");
            var IngDialog = new IngredientDialog();
            IngDialog.Ingredient = new Ingredient();
            var result = IngDialog.ShowDialog();
            if ((bool)result) {
                Ingredients.Add(IngDialog.Ingredient);
            }
            
        }

        private void mniEditIngredient(object sender, RoutedEventArgs e) {
            var selected =(Ingredient)lstIngredients.SelectedItem;
            if (selected == null) { return; }
            Debug.WriteLine($"Editing Ingredient '{selected.Name}'");
            var IngDialog = new IngredientDialog();
            IngDialog.Ingredient = (Ingredient)selected.Clone();
            bool result = (bool)IngDialog.ShowDialog();
            if (result) {
                selected.Name= IngDialog.Ingredient.Name;
                selected.IngredientType= IngDialog.Ingredient.IngredientType;
                selected.PackageType= IngDialog.Ingredient.PackageType;
                selected.Price= IngDialog.Ingredient.Price;
            }
        }
    }
}
