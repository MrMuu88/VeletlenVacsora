﻿using System.Collections.Generic;
using VeletlenVacsora.Api.ViewModels;

namespace VeletlenVacsora.Api.Services
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
