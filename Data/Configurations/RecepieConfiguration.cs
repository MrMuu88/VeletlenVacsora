using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeletlenVacsora.Data.Models;

namespace VeletlenVacsora.Data.Configurations
{
	class RecepieConfiguration:BaseModelConfiguration<Recepie>
	{
		public override void Configure(EntityTypeBuilder<Recepie> builder)
		{
			base.Configure(builder);
		}
	}
}
