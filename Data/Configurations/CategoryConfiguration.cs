using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeletlenVacsora.Data.Models;

namespace VeletlenVacsora.Data.Configurations
{
	class CategoryConfiguration:BaseModelConfiguration<Category>
	{
		public override void Configure(EntityTypeBuilder<Category> builder)
		{
			base.Configure(builder);
		}
	}
}
