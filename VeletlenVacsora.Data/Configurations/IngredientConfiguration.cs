using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeletlenVacsora.Data.Models;

namespace VeletlenVacsora.Data.Configurations
{
	class IngredientConfiguration:BaseModelConfiguration<Ingredient>
	{
		public override void Configure(EntityTypeBuilder<Ingredient> builder)
		{
			base.Configure(builder);
		}
	}
}
