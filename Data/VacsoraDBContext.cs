using Microsoft.EntityFrameworkCore;
using System;

namespace VeletlenVacsora.Data {


	public class VacsoraDBContext : DbContext {
		private static string cnstr;
		private static DBType DBType;

		public DbSet<Recepie> Recepies { get; set; }
		public DbSet<Ingredient> Ingredients { get; set; }
		public DbSet<Category> Categories { get; set; }

		public VacsoraDBContext() {
			cnstr = "Data Source=..\\Vacsora.db;";
			DBType = DBType.SqLite;
		}

		public VacsoraDBContext(string ConnectionString, DBType dbt) {
			cnstr = ConnectionString;
			DBType = dbt;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			switch (DBType) {
				case DBType.MSSql:
					optionsBuilder.UseSqlServer(cnstr);
					break;
				case DBType.MySql:
					optionsBuilder.UseMySQL(cnstr);
					break;
				case DBType.SqLite:
					optionsBuilder.UseSqlite(cnstr);
					break;
				default:
					throw new Exception($"DB Type \"{DBType.ToString()}\" Unknown");
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {

			modelBuilder.Entity<Recepie>().Property(r => r.Name).IsRequired().HasMaxLength(50);

			modelBuilder.Entity<Ingredient>().Property(i => i.Name).IsRequired().HasMaxLength(50);

			modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(25);

			modelBuilder.Entity<RecepieIngredient>()
				.HasKey(t => new { t.RecepieID, t.IngredientID });
		}

	}//clss

	public enum DBType { MSSql, SqLite, MySql }
}//ns
