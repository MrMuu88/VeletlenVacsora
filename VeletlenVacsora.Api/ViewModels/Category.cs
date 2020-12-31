using VeletlenVacsora.Data.Models;

namespace VeletlenVacsora.Api.ViewModels
{
	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public CategoryType Type { get; set; }

		public Category() { }

	}


}
