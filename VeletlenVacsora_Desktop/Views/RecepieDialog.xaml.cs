using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
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
            Debug.WriteLine($"Saving '{Recepie.RecepieID}-{Recepie.Name}' to DB");
            try {
                using (var DB = new VacsoraDBContext(Properties.Settings.Default.constr, DBType.MySql)) {
                    DB.Update(Recepie);
                    DB.SaveChanges();
                }
                Debug.WriteLine("success");
            } catch (Exception ex) {
                Debug.WriteLine($"ERROR: {ex.GetType().Name} - {ex.Message}");
                MessageBox.Show($"ERROR when saving '{Recepie.RecepieID}-{Recepie.Name}'\n{ex.Message}",ex.GetType().Name,MessageBoxButton.OK,MessageBoxImage.Error);
            }
            DialogResult = true;
        }

        private void btnAddIngredient(object sender, RoutedEventArgs e) {
            Debug.WriteLine(Ingredients.Count);
            Recepie.RecepieIngredients.Add(new RecepieIngredient(Recepie, new Ingredient()));
        }

        private void RemoveSelectedrecepie(object sender, MouseButtonEventArgs e) {
            //Recepie.RecepieIngredients.Remove((Ingredient)lstRecepieIngredients.SelectedItem);
        }

        private void AddSelected_recepie(object sender, MouseButtonEventArgs e) {
            var ingredient = (Ingredient)lstIngredients.SelectedItem;

            if (ingredient.IngredientID != 0 &&
                Recepie.RecepieIngredients
                .Select(ri => ri.Ingredient.IngredientID == ingredient.IngredientID)
                .Any()) { return; }
            Recepie.RecepieIngredients.Add(new RecepieIngredient(Recepie, (Ingredient)lstIngredients.SelectedItem));
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
            var selected = (Ingredient)lstIngredients.SelectedItem;
            if (selected == null) { return; }
            Debug.WriteLine($"Editing Ingredient '{selected.Name}'");
            var IngDialog = new IngredientDialog();
            IngDialog.Ingredient = (Ingredient)selected.Clone();
            bool result = (bool)IngDialog.ShowDialog();
            if (result) {
                selected.Name = IngDialog.Ingredient.Name;
                selected.IngredientType = IngDialog.Ingredient.IngredientType;
                selected.PackageType = IngDialog.Ingredient.PackageType;
                selected.Price = IngDialog.Ingredient.Price;
            }
        }
    }
}
