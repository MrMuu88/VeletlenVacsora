using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeletlenVacsora.Data.Models;

namespace VeletlenVacsora.Api.Services
{
	public class RandomMenuGenerator : IMenuGenerator
	{
		private IVacsoraRepository _repo;

		public RandomMenuGenerator(IVacsoraRepository repo)
		{
			_repo = repo;
		}

		public IEnumerable<Recepie> GetMenu(int days)
		{
			//generate the Menu randomly
			return new Recepie[0];
		}
	}
}
