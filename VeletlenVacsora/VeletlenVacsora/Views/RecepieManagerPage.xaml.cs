using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VeletlenVacsora.Views {
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RecepieManagerPage : ContentPage {

		public RecepieManagerPage() {
			InitializeComponent();
		}

		protected override void OnAppearing() {
			base.OnAppearing();
			VModel.Refresh();
		}

	}
}