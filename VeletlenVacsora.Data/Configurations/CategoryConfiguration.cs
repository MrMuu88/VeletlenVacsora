using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeletlenVacsora.Data.Models;

namespace VeletlenVacsora.Data.Configurations
{
	class CategoryConfiguration:BaseModelConfiguration<CategoryModel>
	{
		public override void Configure(EntityTypeBuilder<CategoryModel> builder)
		{
			base.Configure(builder);
			builder.ToTable("Categories");
		}
	}
}
