using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VacsoraDataModel;
using VeletlenVacsora.DependecyServices;
using VeletlenVacsora.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace VeletlenVacsora {
	public partial class App : Application{

		public static ILog Logger;
		public static string ConnString = "Server = simbir.asuscomm.com; UID = Szakacs; PWD = MitFozzunk; database = VacsoraDB; Port = 3306";
		public static DBType DBType = DBType.MySql;

		public App(){
			InitializeComponent();
			Logger = DependencyService.Get<ILog>();
			MainPage = new MainNavigation();
		}

		public static async void LogException(Exception Ex) {
			Logger.MakeLog($"{Ex.GetType().Name}: {Ex.Message}", DependecyServices.LogType.Error);
			await Current.MainPage.DisplayAlert(Ex.GetType().Name, Ex.Message, "Ok");
			if (Ex.InnerException != null) {
				Logger.MakeLog($"{Ex.InnerException.GetType().Name}: {Ex.InnerException.Message}", DependecyServices.LogType.Error);
				await Current.MainPage.DisplayAlert(Ex.InnerException.GetType().Name, Ex.InnerException.Message, "Ok");
			}
		}

		protected override void OnStart(){
			// Handle when your app starts
			Task.Run(()=> {
				Debug.WriteLine("start DB warmup");
				using (var DB = new VacsoraDBContext(ConnString, DBType)) {
					DB.Foods.FirstOrDefault();
				}
				Debug.WriteLine("Finished DB warmup");
			});
		}

		protected override void OnSleep(){ 
			// Handle when your app sleeps
		}

		protected override void OnResume (){
			// Handle when your app resumes
		}
	}//clss
}//ns
