using System.Collections.Generic;

namespace VeletlenVacsora.Api.ViewModels
{
	public class Recepie
	{
		public int id { get; set; }
		public string Name { get; set; }
		public string Category { get; set; }
		public bool IsOnMenu { get; set; }
		public ICollection<RecepieIngredient> Ingredients { get; set; }
		public Recepie() { }

	}//clss
}//ns
