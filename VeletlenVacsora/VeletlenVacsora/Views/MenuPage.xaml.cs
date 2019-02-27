using System;
using Xamarin.Forms;

namespace VeletlenVacsora.Views{
	public partial class MenuPage : ContentPage	{
		public MenuPage(){
			InitializeComponent();
		}

		protected override void OnAppearing() {
			base.OnAppearing();
			VModel.Refresh();
		}

		private void OnPinImage_Tapped(object sender, EventArgs e) {

		}
	}
}
