using System;
using VacsoraDataModel;

namespace DBCreator {
	class Program {
		static void Main(string[] args) {
			Console.WriteLine("Hello World!");
			Console.WriteLine("Opening DB Connection");
			var DBctx = new VacsoraDBContext("Server = simbir.asuscomm.com; UID = Szakacs; PWD = MitFozzunk; database = VacsoraDB; Port = 3306", DBType.MySql);
			//var DBctx = new VacsoraDBContext(@"Data Source = d:\Vacsora.db");
			Console.WriteLine("Connecetion opened\nDeleting old content");
			DBctx.Database.EnsureDeleted();
			Console.WriteLine("Done... recreating content");
			DBctx.Database.EnsureCreated();
			DBctx.Foods.Add(new Food("Paprikás krumpli", 90));
			DBctx.Foods.Add(new Food("Palacsinta", 90));
			DBctx.Foods.Add(new Food("Rizottó", 90));
			DBctx.Foods.Add(new Food("Tojásos lecsó", 90));
			DBctx.Foods.Add(new Food("Gombóc", 90));
			DBctx.Foods.Add(new Food("Gulyás leves", 90));
			DBctx.Foods.Add(new Food("Csirkepörkölt", 90));
			DBctx.Foods.Add(new Food("Sült csirke", 90));
			DBctx.Foods.Add(new Food("Borsó főzelék", 90));
			DBctx.Foods.Add(new Food("Borsó leves", 90));
			DBctx.Foods.Add(new Food("Brokkoli krémleves", 90));
			DBctx.Foods.Add(new Food("Fokhagyma krémleves", 90));
			DBctx.Foods.Add(new Food("rakott karfiol", 90));
			DBctx.Foods.Add(new Food("Paradicsomos Húsgombóc", 90));
			DBctx.SaveChanges();
			Console.WriteLine("Done... Press any key to continue...");
			Console.ReadKey();
		}
	}
}
