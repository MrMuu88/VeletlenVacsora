﻿using Microsoft.AspNetCore.Mvc;
using VeletlenVacsora.Data.Models;
using VeletlenVacsora.Data.Repositories;

namespace VeletlenVacsora.Web.Controllers
{
	[ApiController]
	[Route("Api/[Controller]")]
	public class IngredientsController : BaseModelController<Ingredient>
	{
		public IngredientsController(IRepository<Ingredient> repo) : base(repo)
		{
		}
	}
}
