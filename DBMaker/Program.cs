using System;
using VeletlenVacsora.Data;

namespace DBCreator {
	class Program {
		static void Main(string[] args) {
			string Constr = "";
			DBType dBType = DBType.MSSql;
			Console.WriteLine("please select witch DB to build:\n");
			Console.WriteLine("1\t MSSql");
			Console.WriteLine("2\t MySql");
			Console.WriteLine("3\t SqLite");
			Console.WriteLine("Anithing else will Cancel");

			var ans = Console.ReadKey();
			switch (ans.Key) {
				case ConsoleKey.D1:
					Constr = "Server=Localhost;Database=VacsoraDB;Trusted_Connection=True;";
					dBType = DBType.MSSql;
					break;
				case ConsoleKey.D2:
					Constr = "Server = simbir.asuscomm.com; UID = Szakacs; PWD = MitFozzunk; database = VacsoraDB; Port = 3306";
					dBType = DBType.MySql;
					break;
				case ConsoleKey.D3:
					Constr = @"Data source=.\test.sqlite";
					dBType = DBType.SqLite;
					break;
				default:
					Environment.Exit(0);
					break;
			}
			Console.WriteLine($"Building {dBType.ToString()} Database");
			
			VacsoraDBContext DB = new VacsoraDBContext(Constr, dBType);

			DB.Database.EnsureDeleted();
			DB.Database.EnsureCreated();

			Console.WriteLine("defining recepie objects");

			Recepie Palacsinta = new Recepie("Palacsinta");
			Recepie PaprikasKrumpli = new Recepie("PaprikásKrumpli");
			Recepie Lecso = new Recepie("Lecsó");

			Console.WriteLine("defining Ingredient Tipes objects");

			Category Tejtermek = new Category("Tejtermékek",CategoryType.Ingredient);
			Category AlapVeto = new Category("Alapvető élelmiszerek", CategoryType.Ingredient);
			Category Zoldseg = new Category("Zöldségek", CategoryType.Ingredient);
			Category FeldHus = new Category("Feldolgozott húsok", CategoryType.Ingredient);
			Category Tartos = new Category("Tartós élelmiszerek", CategoryType.Ingredient);

			Console.WriteLine("defining Package tipes objects");

			Category Liter = new Category("Liter", CategoryType.Package);
			Category Doboz = new Category("Doboz", CategoryType.Package);
			Category Kilogram = new Category("Kg", CategoryType.Package);
			Category Csomag = new Category("Csomag", CategoryType.Package);
			Category Uveg = new Category("Üveg", CategoryType.Package);


			Console.WriteLine("defining Ingredients objects");

			Ingredient Tej = new Ingredient("Tej", Tejtermek, Liter, 350);
			Ingredient Tojas = new Ingredient("Tojás", AlapVeto, Doboz, 300);
			Ingredient Liszt = new Ingredient("Liszt", AlapVeto, Kilogram, 150);
			Ingredient Hagyma = new Ingredient("Hagyma", Zoldseg, Kilogram, 350);
			Ingredient Krumpli = new Ingredient("Krumpli", Zoldseg, Kilogram, 250);
			Ingredient Virsli = new Ingredient("Virsli", FeldHus, Csomag, 300);
			Ingredient UvegesLecso = new Ingredient("Üveges Lecsó", Tartos, Uveg, 350);
			Ingredient LecsoKolbasz = new Ingredient("Lecsó Kolbász", FeldHus, Csomag, 700);

			Console.WriteLine("creating relations between Recepies and ingredients");

			Palacsinta.Ingredients.Add(new RecepieIngredient(Palacsinta, Tej, 1.0d));
			Palacsinta.Ingredients.Add(new RecepieIngredient(Palacsinta, Tojas, 0.2d));
			Palacsinta.Ingredients.Add(new RecepieIngredient(Palacsinta, Liszt, 0.25d));
			PaprikasKrumpli.Ingredients.Add(new RecepieIngredient(PaprikasKrumpli, Hagyma, 0.2d));
			PaprikasKrumpli.Ingredients.Add(new RecepieIngredient(PaprikasKrumpli, Krumpli, 0.5d));
			PaprikasKrumpli.Ingredients.Add(new RecepieIngredient(PaprikasKrumpli, Virsli, 1.0d));
			Lecso.Ingredients.Add(new RecepieIngredient(Lecso, Hagyma, 0.2d));
			Lecso.Ingredients.Add(new RecepieIngredient(Lecso, UvegesLecso, 1.0d));
			Lecso.Ingredients.Add(new RecepieIngredient(Lecso, LecsoKolbasz, 0.5d));
			Lecso.Ingredients.Add(new RecepieIngredient(Lecso, Tojas, 0.5d));

			Console.WriteLine("Adding recepies to DB");

			DB.Recepies.Add(Palacsinta);
			DB.Recepies.Add(PaprikasKrumpli);
			DB.Recepies.Add(Lecso);

			Console.WriteLine("saving changes to DB");
			DB.SaveChanges();


			Console.WriteLine("Done");
			Console.ReadKey();
		}
	}
}
