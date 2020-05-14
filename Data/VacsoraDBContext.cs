using Microsoft.EntityFrameworkCore;
using System;
using VeletlenVacsora.Data.Configurations;
using VeletlenVacsora.Data.Models;

namespace VeletlenVacsora.Data {


	public class VacsoraDbContext : DbContext {

		public DbSet<Recepie> Recepies { get; set; }
		public DbSet<Ingredient> Ingredients { get; set; }

		public DbSet<Category> Categories { get; set; }

		public VacsoraDbContext(DbContextOptions options):base(options) {}


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {

			modelBuilder.ApplyConfiguration(new RecepieConfiguration());
			modelBuilder.ApplyConfiguration(new IngredientConfiguration());
			modelBuilder.ApplyConfiguration(new CategoryConfiguration());

			modelBuilder.Entity<RecepieIngredient>()
				.HasKey(t => new { t.RecepieID, t.IngredientID });
		}

	}//clss

}//ns
