using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using VeletlenVacsora.Data.Models;
using VeletlenVacsora.Data.Repositories;

namespace VeletlenVacsora.Api.Controllers
{
	[Route("api/[controller]/[Action]")]
	[ApiController]
	public class MenuController : ControllerBase
	{
		public ILogger<MenuController> Logger { get; }
		public IRepository<RecepieModel> Repository { get; }

		public MenuController(ILogger<MenuController> logger,IRepository<RecepieModel> repository)
		{
			Logger = logger;
			Repository = repository;
		}


		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<RecepieModel>),200)]
		[ProducesResponseType(500)]
		public async Task<ActionResult<IEnumerable<RecepieModel>>> GetCurrentMenu()
		{
			try
			{
				var menu = await Repository.FindAsync(r => r.IsOnMenu);
				return Ok(menu);
			}
			catch (Exception ex)
			{
				var methodInfo = MethodBase.GetCurrentMethod();
				Logger.LogError(ex, $"An Exception occured in {methodInfo.DeclaringType.Name}.{methodInfo.Name}");
				var errorobj = new { Error = ex.GetType().Name, ex.Message };
				return StatusCode(StatusCodes.Status500InternalServerError, errorobj);
			}
		}
			
	}
}
