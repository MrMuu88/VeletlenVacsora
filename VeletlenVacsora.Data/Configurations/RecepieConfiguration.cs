using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeletlenVacsora.Data.Models;

namespace VeletlenVacsora.Data.Configurations
{
	class RecepieConfiguration:BaseModelConfiguration<RecepieModel>
	{
		public override void Configure(EntityTypeBuilder<RecepieModel> builder)
		{
			base.Configure(builder);
			builder.ToTable("Recepies");
			builder.Property(r => r.Weight).HasDefaultValue(1);
		}
	}
}
