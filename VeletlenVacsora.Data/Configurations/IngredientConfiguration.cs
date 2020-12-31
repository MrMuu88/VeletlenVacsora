using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeletlenVacsora.Data.Models;

namespace VeletlenVacsora.Data.Configurations
{
	class IngredientConfiguration:BaseModelConfiguration<IngredientModel>
	{
		public override void Configure(EntityTypeBuilder<IngredientModel> builder)
		{
			base.Configure(builder);
			builder.ToTable("Ingredients");
		}
	}
}
