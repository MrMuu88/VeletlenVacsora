using Microsoft.EntityFrameworkCore;
using System;
using VeletlenVacsora.Data.Models;

namespace VeletlenVacsora.Data {


	public class VacsoraDBContext : DbContext {

		public DbSet<Recepie> Recepies { get; set; }
		public DbSet<Ingredient> Ingredients { get; set; }

		public DbSet<Category> Categories { get; set; }

		public VacsoraDBContext(DbContextOptions options):base(options) {}


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {

			modelBuilder.Entity<Recepie>().Property(r => r.Name).IsRequired().HasMaxLength(50);

			modelBuilder.Entity<Ingredient>().Property(i => i.Name).IsRequired().HasMaxLength(50);

			modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(25);

			modelBuilder.Entity<RecepieIngredient>()
				.HasKey(t => new { t.RecepieID, t.IngredientID });
		}

	}//clss

}//ns
