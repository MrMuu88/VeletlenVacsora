using GalaSoft.MvvmLight.Command;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VeletlenVacsora.Data;
using VeletlenVacsora.Desktop.Views;

namespace VeletlenVacsora.Desktop.ViewModels {
    class MainWindow_VM : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        public static VacsoraDBContext DB { get; set; } = new VacsoraDBContext(Properties.Settings.Default.constr, DBType.MySql);

        #region Fields and Propertties ############################################################
        private ObservableCollection<Recepie> _Menu;
        private ObservableCollection<Ingredient> _ShopingList;
        private ObservableCollection<Recepie> _Recepies;
        private ObservableCollection<Ingredient> _Ingredients;

        public ObservableCollection<Recepie> Menu {
            get { return _Menu; }
            set { _Menu = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Menu))); }
        }

        public ObservableCollection<Recepie> Recepies {
            get { return _Recepies; }
            set { _Recepies = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Recepies))); }
        }

        //public ObservableCollection<Ingredient> Ingredients {
        //    get { return _Ingredients; }
        //    set { _Ingredients = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Ingredients))); }
        //}
        public ObservableCollection<Ingredient> Ingredients {
            get { return DB.Ingredients.Local.ToObservableCollection(); }
            set { DB.Ingredients.Local = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Ingredients))); }
        }

        public ObservableCollection<Ingredient> ShopingList {
            get { return _ShopingList; }
            set { _ShopingList = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShopingList))); }
        }

        public ICommand cmdNewRecepie { get; private set; }
        public ICommand cmdRemoveRecepie { get; private set; }
        public ICommand cmdEditRecepie { get; private set; }

        public ICommand cmdNewIngredient { get; private set; }
        public ICommand cmdRemoveIngredient { get; private set; }
        public ICommand cmdEditIngredient { get; private set; }

        public ICommand cmdsave{get;private set;}

        #endregion

        #region ctors #############################################################################

        public MainWindow_VM() {
            Menu = new ObservableCollection<Recepie>();
            ShopingList = new ObservableCollection<Ingredient>();
            Recepies = new ObservableCollection<Recepie>();
            Ingredients = new ObservableCollection<Ingredient>();

            cmdNewRecepie    = new RelayCommand(NewRecepie);
            cmdRemoveRecepie = new RelayCommand<Recepie>(RemoveRecepie);
            cmdEditRecepie = new RelayCommand<Recepie>(EditRecepie);

            cmdNewIngredient    = new RelayCommand(newIngredient);
            cmdRemoveIngredient = new RelayCommand<Ingredient>(RemoveIngredient);
            cmdEditIngredient = new RelayCommand<Ingredient>(EditIngredient);

            cmdsave = new RelayCommand(SaveToDB);

            DB.Recepies.Include(r => r.RecepieIngredients).Load();
            Recepies = new ObservableCollection<Recepie>(DB.Recepies.Local);

            DB.Ingredients.Load();
            DB.Ingredients.Local.;
            Ingredients = new ObservableCollection<Ingredient>(DB.Ingredients.Local);

        }


        #endregion

        #region Methods and commands ##############################################################


        private void NewRecepie() {
            Debug.WriteLine("Creating new recepie");
            var RecDialog = new RecepieDialog();
            RecDialog.Recepie = new Recepie();
            RecDialog.Ingredients = Ingredients;

            var Result = (bool)RecDialog.ShowDialog();
            if (Result) {
                Recepies.Add(RecDialog.Recepie);
            }
        }

        private void RemoveRecepie(Recepie selected) {
            if(selected == null) { return; }
            Debug.WriteLine($"Removing recepie '{selected.Name}'");
            Recepies.Remove(selected);
        }

        private void EditRecepie(Recepie selected) {
            if (selected == null) { return; }
            Debug.WriteLine($"Editing recepie '{selected.Name}'");
            var RecDialog = new RecepieDialog();
            RecDialog.Recepie = (Recepie)selected.Clone();
            RecDialog.Ingredients = Ingredients;

            var Result = (bool)RecDialog.ShowDialog();
            if (Result) {
                selected.Name = RecDialog.Recepie.Name;
                selected.RecepieIngredients = RecDialog.Recepie.RecepieIngredients;  
            }
        }



        private void newIngredient() {
            Debug.WriteLine("Creating new Ingredient");
            var IngDialog =new IngredientDialog();
            IngDialog.Ingredient = new Ingredient();
            var result =(bool) IngDialog.ShowDialog();
            if (result) {
                Ingredients.Add(IngDialog.Ingredient);
                DB.Ingredients.Add(IngDialog.Ingredient);
                DB.SaveChanges();
            }
           
        }
        
        private void EditIngredient(Ingredient selected) {
            if (selected == null) { return; }
            Debug.WriteLine($"Editing Ingredient '{selected.Name}'");
            var IngDialog = new IngredientDialog();
            IngDialog.Ingredient = (Ingredient)selected;
            bool result = (bool)IngDialog.ShowDialog();
            if (result) {
                DB.Ingredients.Update(IngDialog.Ingredient);
                DB.SaveChanges();
            }
        }

        private void RemoveIngredient(Ingredient selected) {
            if (selected == null) { return; }
            Debug.WriteLine($"Removing Ingredient '{selected.Name}'");
            Ingredients.Remove(selected);
            DB.Ingredients.Remove(selected);
            DB.SaveChanges();
        }


        private void SaveToDB() {
        //    Debug.WriteLine("Updating DB");
        //    using (VacsoraDBContext DB = new VacsoraDBContext(Properties.Settings.Default.constr, DBType.MySql)) {
        //        DB.Ingredients.UpdateRange(Ingredients);
        //        DB.Recepies.UpdateRange(Recepies);
        //        DB.SaveChanges();
        //    }
        //    Debug.WriteLine("Done");
        }
        
        #endregion
    }
}
