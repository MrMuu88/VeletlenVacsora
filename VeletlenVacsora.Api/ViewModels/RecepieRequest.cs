using System;

namespace VeletlenVacsora.Api.ViewModels
{
	public class RecepieRequest
	{
		public string Name { get; set; }
		public int Weight { get; set; }
		public DateTime? LastOnMenu { get; set; }
	}
}
