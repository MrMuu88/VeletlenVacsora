using System;

namespace VeletlenVacsora.Data {
	public class Program {

		#region fileds, Properties, events ########################################################

		#endregion

		#region Ctors #############################################################################
		public Program() { }
		#endregion

		#region Methods, Tasks ####################################################################
		public static void Main() {
			Console.WriteLine("runing Data Program, to seed te database with test data");
			Seed();
			Console.ReadKey();
		}

		public static void Seed() {
			//TODO implement Seeding logic
		}
		#endregion
	}
}
