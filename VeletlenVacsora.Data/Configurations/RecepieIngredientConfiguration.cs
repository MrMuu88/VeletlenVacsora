using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VeletlenVacsora.Data.Models;

namespace VeletlenVacsora.Data.Configurations
{
	public class RecepieIngredientConfiguration : IEntityTypeConfiguration<RecepieIngredientModel>
	{
		public void Configure(EntityTypeBuilder<RecepieIngredientModel> builder)
		{
			builder.HasKey(ri => new { ri.RecepieId, ri.IngredientId });
			builder.HasOne(ri => ri.Recepie).WithMany(r => r.Ingredients).HasForeignKey(ri=> ri.RecepieId);
			builder.HasOne(ri => ri.Ingredient).WithMany(i => i.Recepies).HasForeignKey(ri => ri.IngredientId);
			builder.ToTable("RecepieIngredients");
		}
	}
}
