using System;
using VacsoraDataModel;

namespace DBCreator {
	class Program {
		static void Main(string[] args) {
			Console.WriteLine("Hello World!");
			Console.WriteLine("Opening DB Connection");
			var DBctx = new VacsoraDBContext("Server = 192.168.1.9; UID = root; PWD = RXMdnq88; database = VacsoraDB; Port = 3306", DBType.MySql);
			//var DBctx = new VacsoraDBContext(@"Data Source = d:\Vacsora.db");
			Console.WriteLine("Connecetion opened\nDeleting old content");
			DBctx.Database.EnsureDeleted();
			Console.WriteLine("Done... recreating content");
			DBctx.Database.EnsureCreated();
			DBctx.Foods.Add(new Food("Paprikás krumpli", 50));
			DBctx.Foods.Add(new Food("Palacsinta", 50));
			DBctx.Foods.Add(new Food("Rizottó", 50));
			DBctx.Foods.Add(new Food("Tojásos lecsó", 50));
			DBctx.Foods.Add(new Food("Gombóc", 50));
			DBctx.Foods.Add(new Food("Gulyás leves", 50));
			DBctx.Foods.Add(new Food("Csirkepörkölt", 50));
			DBctx.Foods.Add(new Food("Sült csirke", 50));
			DBctx.Foods.Add(new Food("Borsó főzelék", 50));
			DBctx.Foods.Add(new Food("Borsó leves", 50));
			DBctx.Foods.Add(new Food("Brokkoli krémleves", 50));
			DBctx.Foods.Add(new Food("Fokhagyma krémleves", 50));
			DBctx.Foods.Add(new Food("rakott karfiol", 50));
			DBctx.Foods.Add(new Food("Paradicsomos Húsgombóc", 50));
			DBctx.SaveChanges();
			Console.WriteLine("Done... Press any key to continue...");
			Console.ReadKey();
		}
	}
}
