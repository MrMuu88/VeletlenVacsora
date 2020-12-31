﻿using Microsoft.AspNetCore.Mvc;
using VeletlenVacsora.Data.Models;
using VeletlenVacsora.Data.Repositories;

namespace VeletlenVacsora.Api.Controllers
{
	[ApiController]
	[Route("Api/[Controller]")]
	public class RecepiesController : BaseModelController<Recepie>
	{
		public RecepiesController(IRepository<Recepie> repo) : base(repo)
		{
		}
	}
}