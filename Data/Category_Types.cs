using System.ComponentModel;

namespace VeletlenVacsora.Data {
	public class Category:INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;

		public virtual int ID { get; set; }


		private string _Name;
		public string Name {
			get { return _Name; }
			set {
				_Name = value;
				RaisePropertyChanged(nameof(Name));
			}
		}


		private CategoryType _Type;
		public CategoryType Type {
			get { return _Type; }
			set {
				_Type = value;
				RaisePropertyChanged(nameof(Type));
			}
		}

		public Category() {
			Name = "new Category";
			Type = CategoryType.Ingredient;
		}

		public Category(string name, CategoryType type) {
			Name = name;
			Type = type;
		}

		private void RaisePropertyChanged(string PropertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		}

	}

	public enum CategoryType { Ingredient,Package,Recepie}
}
