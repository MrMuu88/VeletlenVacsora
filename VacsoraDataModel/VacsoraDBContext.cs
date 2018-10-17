using System;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;
using MySql.Data.MySqlClient;

namespace VacsoraDataModel {

	
	public class VacsoraDBContext : DbContext {
		private static string cnstr;
		private static DBType DBType;

		public DbSet<Food> Foods { get; set; }
		public DbSet<Ingredient> Ingredients { get; set; }

		public VacsoraDBContext(string ConnectionString, DBType dbt = DBType.SqLite) {
			cnstr = ConnectionString;
			DBType = dbt;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			switch (DBType) {
				case DBType.MySql:
					optionsBuilder.UseMySql(cnstr);
					break;
				case DBType.SqLite:
					optionsBuilder.UseSqlite(cnstr);
					break;
				default:
					throw new Exception($"DB Type \"{DBType.ToString()}\" Unknown");
			}
		}

		
	}//clss

	public enum DBType {SqLite,MySql}
}//ns
