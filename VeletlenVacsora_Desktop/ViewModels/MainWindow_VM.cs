using GalaSoft.MvvmLight.Command;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using VeletlenVacsora.Data;
using VeletlenVacsora.Desktop.Views;

namespace VeletlenVacsora.Desktop.ViewModels {
	class MainWindow_VM : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
			   
		#region Fields and Propertties ############################################################
		private Random Dice;
		private Recepie[] _Menu;
        private ObservableCollection<RecepieIngredient> _ShopingList;
        private ObservableCollection<Recepie> _Recepies;
        private ObservableCollection<Ingredient> _Ingredients;


		public Recepie[] Menu {
			get { return _Menu; }
			set {
				_Menu = value;
				RaisePropertyChanged(nameof(Menu));
			}
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
		

        public ObservableCollection<RecepieIngredient> ShopingList {
            get { return _ShopingList; }
            set { _ShopingList = value;
				  RaisePropertyChanged(nameof(ShopingList)); }
        }
		
        public ICommand cmdRemoveRecepie { get; private set; }
        public ICommand cmdEditRecepie { get; private set; }
		
        public ICommand cmdRemoveIngredient { get; private set; }
        public ICommand cmdEditIngredient { get; private set; }

		public ICommand cmdRollMenu{ get; private set; }

        public ICommand cmdsave{get;private set;}

        #endregion

        #region ctors #############################################################################

        public MainWindow_VM() {
			Dice = new Random();
			App.DB = new VacsoraDBContext(Properties.Settings.Default.constr, Properties.Settings.Default.dbtype);
			
            ShopingList = new ObservableCollection<RecepieIngredient>();
            Recepies = new ObservableCollection<Recepie>();
			
            cmdRemoveRecepie = new RelayCommand<Recepie>(RemoveRecepie);
            cmdEditRecepie = new RelayCommand<Recepie>(EditRecepie);

            
            cmdRemoveIngredient = new RelayCommand<Ingredient>(RemoveIngredient);
            cmdEditIngredient = new RelayCommand<Ingredient>(EditIngredient);

			cmdRollMenu = new RelayCommand(RollMenu);

            App.DB.Recepies.Include(r => r.Ingredients).Load();

            App.DB.Ingredients.Load();

			App.DB.Categories.Load();

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
			App.SaveToDB();
        }

		public void EditRecepie(Recepie selected) {
			if (selected == null) { selected = new Recepie(); }
			Debug.WriteLine($"Editing recepie '{selected.Name}'");
			var RecDialog = new RecepieDialog();
			var DDContext = (RecepieDialogVM)RecDialog.DataContext;
			DDContext.Recepie = selected;
			DDContext.Ingredients = App.DB.Ingredients.Local.ToObservableCollection(); ;

			var Result = (bool)RecDialog.ShowDialog();
			if (Result) {
				App.DB.Recepies.Update(DDContext.Recepie);
				if (!App.SaveToDB()) {
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
            
            bool result = (bool)IngDialog.ShowDialog();
            if (result) {
                App.DB.Ingredients.Update(DDContext.Ingredient);
				if (!App.SaveToDB()) {
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
			App.SaveToDB();
        }

		
		private void RollMenu() {
			Debug.WriteLine("Rolling New Menu");
			

			Menu = App.DB.Recepies.Local.OrderByDescending(r => r.Weight).Take(7).ToArray();

			//TODO Ingreds list propbably can be replaced by a linq query
			List<RecepieIngredient> Ingreds = new List<RecepieIngredient>();
			
			foreach (Recepie r in Menu) {
				Ingreds.AddRange(r.Ingredients);
			}

			//TODO filtering a unique list of ingredients with accumulated amounts has to be implemented

			//ERROR Not working because of the equality checks are off

			var Unique = new List<RecepieIngredient>();
			foreach (var ri in Ingreds) {
				var item = new RecepieIngredient(new Recepie(), ri.Ingredient,ri.Amount);
				if (!Unique.Contains(item)) {
					Unique.Add(item);
				} else {
					var i = Unique.IndexOf(item);
					Unique[i].Amount += item.Amount;
				}
			}
			
			//TODO Group Shopping List by Ingredient type

			ShopingList = new ObservableCollection<RecepieIngredient>(Unique);

		}

		
		private void RaisePropertyChanged(string PropertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		}

		#endregion
	}
}
