using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeletlenVacsora.Data.Models;

namespace VeletlenVacsora.Data.Configurations
{
	public class BaseModelConfiguration<T> : IEntityTypeConfiguration<T> where T:BaseModel
	{
		public virtual void Configure(EntityTypeBuilder<T> builder)
		{
			builder.HasKey(bm => bm.id);
		}
	}
}
