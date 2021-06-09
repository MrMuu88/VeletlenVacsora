using System;

namespace VeletlenVacsora.Data.Models
{
	public class Recepie : BaseModel { 
		public string Name { get; set; }
		public int Weight { get; set; }
		public DateTime? LastOnMenu { get; set; }
	}
}
