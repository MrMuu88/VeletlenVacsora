using System.Collections.Generic;

namespace VeletlenVacsora.Api.ViewModels
{
	public class Recepie
	{

		public Category Category { get; set; }

		public ICollection<RecepieIngredient> Ingredients { get; set; }
		public Recepie() { }

	}//clss
}//ns
