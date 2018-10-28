using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VacsoraDataModel;
using VeletlenVacsora.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace VeletlenVacsora {
	public partial class App : Application{
		public App (){
			InitializeComponent();

			MainPage = new MainPage();
		}

		protected override void OnStart (){
			// Handle when your app starts
			Task.Run(()=> {
				Debug.WriteLine("start DB warmup");
				using (var DB = new VacsoraDBContext("Server = simbir.asuscomm.com; UID = Szakacs; PWD = MitFozzunk; database = VacsoraDB; Port = 3306", DBType.MySql)) {
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
