using Microsoft.EntityFrameworkCore;
using VeletlenVacsora.Data.Models;

namespace VeletlenVacsora.Data
{
	public class VacsoraDbContext:DbContext
	{
		public DbSet<Recepie> Recepies { get; set; }
	}
}
