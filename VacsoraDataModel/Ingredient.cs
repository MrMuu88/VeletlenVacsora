using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VacsoraDataModel {
	public class Ingredient:INotifyPropertyChanged{
		public event PropertyChangedEventHandler PropertyChanged;

		private int _ID;
		[Key]
		public int ID {
			get { return _ID; }
			set { _ID = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ID")); }}

		private string _Name;
		[MaxLength(50)]
		public string Name {
			get { return _Name; }
			set { _Name = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name")); }}

		private PackageType _Package;
		public  PackageType Package {
			get { return _Package; }
			set { _Package = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Package")); }}

		//Per Package
		private int _Price;
		public  int Price {
			get { return _Price; }
			set { _Price = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Price")); }
		}

		//navigation property for EF
		private Food _Food;
		public Food Food {
			get { return _Food; }
			set { _Food = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Food")); }}

	}//clss

	public enum PackageType {db,Doboz,Kg,üveg,Pohár,L}
}//ns
