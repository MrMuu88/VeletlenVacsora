using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeletlenVacsora.Data.Models;

namespace VeletlenVacsora.Web.Services
{
	public interface IMenuGenerator
	{
		/// <summary>
		/// does some stuff
		/// </summary>
		/// <param name="days"></param>
		/// <returns></returns>
		 IEnumerable<Recepie> GetMenu(int days);
	}
}
