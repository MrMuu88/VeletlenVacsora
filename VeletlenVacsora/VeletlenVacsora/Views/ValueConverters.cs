using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace VeletlenVacsora.Views{
	public class ReserveBoolConverter : IValueConverter {

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			return !((bool)value);
		}
		
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			return !((bool)value);
		}
	}

	public class BoolToImageSourceConverter : IValueConverter {

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if ((bool)value) { 
				return "Pin_On.png";
			} else {
				return "Pin_Off.png";
			}
		}
		
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			if (((string)value)=="Pin_On.png") {
				return true;
			} else {
				return false;
			}
		}
	}

}
