using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeletlenVacsora.Data.Models;

namespace VeletlenVacsora.Data.Configurations
{
	class BaseModelConfiguration<T> : IEntityTypeConfiguration<T> where T: BaseModel
	{
		public virtual void Configure(EntityTypeBuilder<T> builder)
		{
			builder.Property(t => t.Name).IsRequired().HasMaxLength(50);
		}
	}
}
