﻿using GalaSoft.MvvmLight.Command;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VeletlenVacsora.Data;
using VeletlenVacsora.Desktop.Views;

namespace VeletlenVacsora.Desktop.ViewModels {
    class MainWindow_VM : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        

        #region Fields and Propertties ############################################################
        private ObservableCollection<Recepie> _Menu;
        private ObservableCollection<Ingredient> _ShopingList;
        private ObservableCollection<Recepie> _Recepies;
        private ObservableCollection<Ingredient> _Ingredients;
		private ObservableCollection<IngredientType> _IngredientTypes;

        public ObservableCollection<Recepie> Menu {
            get { return _Menu; }
            set { _Menu = value;
				  RaisePropertyChanged(nameof(Menu)); }
        }

        public ObservableCollection<Recepie> Recepies {
            get { return _Recepies; }
            set { _Recepies = value;
				  RaisePropertyChanged(nameof(Recepies)); }
        }

       
        public ObservableCollection<Ingredient> Ingredients {
            get { return _Ingredients; }
			set { _Ingredients = value; 
			      RaisePropertyChanged(nameof(Ingredients)); }
        }
		
		public ObservableCollection<IngredientType> IngredientTypes {
			get { return _IngredientTypes; }
			set { _IngredientTypes = value;
				  RaisePropertyChanged(nameof(IngredientTypes)); }
		}


		private ObservableCollection<PackageType> _PackageTypes;
		public ObservableCollection<PackageType> PackageTypes {
			get { return _PackageTypes; }
			set { _PackageTypes = value;
				  RaisePropertyChanged(nameof(PackageTypes)); }
		}

        public ObservableCollection<Ingredient> ShopingList {
            get { return _ShopingList; }
            set { _ShopingList = value;
				  RaisePropertyChanged(nameof(ShopingList)); }
        }
		
        public ICommand cmdRemoveRecepie { get; private set; }
        public ICommand cmdEditRecepie { get; private set; }
		
        public ICommand cmdRemoveIngredient { get; private set; }
        public ICommand cmdEditIngredient { get; private set; }

        public ICommand cmdsave{get;private set;}

        #endregion

        #region ctors #############################################################################

        public MainWindow_VM() {
			App.DB = new VacsoraDBContext(Properties.Settings.Default.constr, Properties.Settings.Default.dbtype);

			Menu = new ObservableCollection<Recepie>();
            ShopingList = new ObservableCollection<Ingredient>();
            Recepies = new ObservableCollection<Recepie>();
			
            cmdRemoveRecepie = new RelayCommand<Recepie>(RemoveRecepie);
            cmdEditRecepie = new RelayCommand<Recepie>(EditRecepie);

            
            cmdRemoveIngredient = new RelayCommand<Ingredient>(RemoveIngredient);
            cmdEditIngredient = new RelayCommand<Ingredient>(EditIngredient);

            //cmdsave = new RelayCommand(SaveToDB);

            App.DB.Recepies.Include(r => r.Ingredients).Load();

            App.DB.Ingredients.Load();
			App.DB.PackageTypes.Load();
			App.DB.IngredientTypes.Load();

            Recepies = new ObservableCollection<Recepie>(App.DB.Recepies.Local);
			Ingredients = new ObservableCollection<Ingredient>(App.DB.Ingredients.Local);

			App.DB.Database.CloseConnection();
        }


        #endregion

        #region Methods and commands ##############################################################

        private void RemoveRecepie(Recepie selected) {
            if(selected == null) { return; }
            Debug.WriteLine($"Removing recepie '{selected.Name}'");
            Recepies.Remove(selected);
			App.DB.Recepies.Remove(selected);
			SaveToDB();
        }

        private void EditRecepie(Recepie selected) {
            if (selected == null) { selected = new Recepie(); }
            Debug.WriteLine($"Editing recepie '{selected.Name}'");
            var RecDialog = new RecepieDialog();
			var DDContext = (RecepieDialogVM)RecDialog.DataContext;
            DDContext.Recepie = selected;
			DDContext.Ingredients = App.DB.Ingredients.Local.ToObservableCollection(); ;

            var Result = (bool)RecDialog.ShowDialog();
            if (Result) {
				App.DB.Recepies.Update(DDContext.Recepie);
				if(!SaveToDB()){ 
					//TODO save failed undo modifications
				}
				Recepies = App.DB.Recepies.Local.ToObservableCollection();
            }
        }
        
        private void EditIngredient(Ingredient selected) {
			
            if (selected == null) { selected = new Ingredient();}
            Debug.WriteLine($"Editing Ingredient '{selected.Name}'");
			var unmodified = selected.Clone();
            var IngDialog = new IngredientDialog();

			var DDContext =(IngredientDialogVM)IngDialog.DataContext;
			DDContext.Ingredient = selected;
			DDContext.IngredientTypes = App.DB.IngredientTypes.Local.ToObservableCollection();
			DDContext.PackageTypes = App.DB.PackageTypes.Local.ToObservableCollection();
            
            bool result = (bool)IngDialog.ShowDialog();
            if (result) {
                App.DB.Ingredients.Update(DDContext.Ingredient);
				if (!SaveToDB()) {
					//TODO save failed undo modifications
				}
				Ingredients = App.DB.Ingredients.Local.ToObservableCollection();
			}
        }

        private void RemoveIngredient(Ingredient selected) {
            if (selected == null) { return; }
            Debug.WriteLine($"Removing Ingredient '{selected.Name}'");
            Ingredients.Remove(selected);
            App.DB.Ingredients.Remove(selected);
			SaveToDB();
        }


        private bool SaveToDB() {
			bool answ = false;
			try {
				Debug.WriteLine("Saving Changes to Database");
				App.DB.Database.OpenConnection();
				App.DB.SaveChanges();
				answ = true;
			} catch (Exception ex) {
				MessageBox.Show($"an error occured while saving modifications to DB:\n{ex.Message}",$"ERROR: {ex.GetType().Name}",MessageBoxButton.OK,MessageBoxImage.Error);
				
			} finally {
				App.DB.Database.CloseConnection();
			}
			return answ;
        
        }

		private void RaisePropertyChanged(string PropertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		}

		#endregion
	}
}
