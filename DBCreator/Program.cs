using System;
using VeletlenVacsora.Data;
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
			DBctx.Recepies.Add(new Recepie("Paprikás krumpli", 90));
			DBctx.Recepies.Add(new Recepie("Palacsinta", 90));
			DBctx.Recepies.Add(new Recepie("Rizottó", 90));
			DBctx.Recepies.Add(new Recepie("Tojásos lecsó", 90));
			DBctx.Recepies.Add(new Recepie("Gombóc", 90));
			DBctx.Recepies.Add(new Recepie("Gulyás leves", 90));
			DBctx.Recepies.Add(new Recepie("Csirkepörkölt", 90));
			DBctx.Recepies.Add(new Recepie("Sült csirke", 90));
			DBctx.Recepies.Add(new Recepie("Borsó főzelék", 90));
			DBctx.Recepies.Add(new Recepie("Borsó leves", 90));
			DBctx.Recepies.Add(new Recepie("Brokkoli krémleves", 90));
			DBctx.Recepies.Add(new Recepie("Fokhagyma krémleves", 90));
			DBctx.Recepies.Add(new Recepie("rakott karfiol", 90));
			DBctx.Recepies.Add(new Recepie("Paradicsomos Húsgombóc", 90));
			DBctx.SaveChanges();
			Console.WriteLine("Done... Press any key to continue...");
			Console.ReadKey();
		}
	}
}
