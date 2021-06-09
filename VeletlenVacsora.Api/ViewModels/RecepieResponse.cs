using System;

namespace VeletlenVacsora.Api.ViewModels
{
	public class RecepieResponse
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Weight { get; set; }
		public DateTime? LastOnMenu { get; set; }
	}
}
