namespace VeletlenVacsora.Data.Models {
	public class RecepieIngredient {

		#region Fields and properties ###################################################################

		public int RecepieID { get; set; }

		public int IngredientID { get; set; }

		public Recepie Recepie { get; set; }

		public Ingredient Ingredient { get; set; }

		public double Amount { get; set; }

		#endregion

		#region Ctors ###################################################################################


		public RecepieIngredient() {

		}

		#endregion

		#region Methods #################################################################################

		#endregion
	}
}
