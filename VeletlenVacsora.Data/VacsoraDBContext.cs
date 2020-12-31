using Microsoft.EntityFrameworkCore;
using VeletlenVacsora.Data.Configurations;
using VeletlenVacsora.Data.Models;

namespace VeletlenVacsora.Data
{


	public class VacsoraDbContext : DbContext {

		public DbSet<RecepieModel> Recepies { get; set; }
		public DbSet<IngredientModel> Ingredients { get; set; }

		public DbSet<CategoryModel> Categories { get; set; }

		public VacsoraDbContext(DbContextOptions options):base(options) {}


		protected override void OnModelCreating(ModelBuilder modelBuilder) {

			modelBuilder.ApplyConfiguration(new RecepieConfiguration());
			modelBuilder.ApplyConfiguration(new IngredientConfiguration());
			modelBuilder.ApplyConfiguration(new CategoryConfiguration());

			modelBuilder.Entity<RecepieIngredientModel>()
				.HasKey(t => new { t.RecepieID, t.IngredientID });
		}

	}//clss

}//ns
