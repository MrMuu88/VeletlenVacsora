﻿using Microsoft.AspNetCore.Mvc;
using VeletlenVacsora.Data.Models;
using VeletlenVacsora.Data.Repositories;

namespace VeletlenVacsora.Api.Controllers
{
	[ApiController]
	[Route("Api/[Controller]")]
	public class CategoriesController : BaseModelController<Category>
	{
		public CategoriesController(IRepository<Category> repo) : base(repo)
		{
		}
	}
}
