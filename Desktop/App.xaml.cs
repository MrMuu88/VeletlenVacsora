using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using VeletlenVacsora.Data;
using VeletlenVacsora.Desktop.ViewModels;
using VeletlenVacsora.Desktop.Views;

namespace VeletlenVacsora.Desktop {
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application {
		public static VacsoraDBContext DB { get; set; }

		public static bool SaveToDB() {
			bool answ = false;
			try {
				Debug.WriteLine("Saving Changes to Database");
				DB.Database.OpenConnection();
				DB.SaveChanges();
				answ = true;
			} catch (Exception ex) {
				MessageBox.Show($"an error occured while saving modifications to DB:\n{ex.Message}", $"ERROR: {ex.GetType().Name}", MessageBoxButton.OK, MessageBoxImage.Error);

			} finally {
				DB.Database.CloseConnection();
			}
			return answ;

		}

	}
}
