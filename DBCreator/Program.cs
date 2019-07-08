using System;

namespace VeletlenVacsora.DBCreator {
	public class Program {

		public static void Main() {
			Console.WriteLine("Starting db seed Program");
			Seed();
			Console.WriteLine("Done\n press any key to exit...");
			Console.ReadKey();

		}

		private static void Seed() {
			//TODO Impleement Seed Logic
			Console.WriteLine("Seeding data to database");

		}
	}
}
