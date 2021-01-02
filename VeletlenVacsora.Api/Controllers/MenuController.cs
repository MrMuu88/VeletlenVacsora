using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using VeletlenVacsora.Api.ViewModels;
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
		public IMapper Mapper { get; }

		public MenuController(ILogger<MenuController> logger,IRepository<RecepieModel> repository,IMapper mapper)
		{
			Logger = logger;
			Repository = repository;
			Mapper = mapper;
		}

		//Returns the currently Set Menu
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<Recepie>),200)]
		[ProducesResponseType(500)]
		public async Task<ActionResult<IEnumerable<Recepie>>> GetCurrentMenu()
		{
			try
			{
				var menu = await Repository.FindAsync(r => r.IsOnMenu);
				var vm = Mapper.Map<IEnumerable<Recepie>>(menu);
				return Ok(vm);
			}
			catch (Exception ex)
			{
				var methodInfo = MethodBase.GetCurrentMethod();
				Logger.LogError(ex, $"An Exception occured in {methodInfo.DeclaringType.Name}.{methodInfo.Name}");
				var errorobj = new { Error = ex.GetType().Name, ex.Message };
				return StatusCode(StatusCodes.Status500InternalServerError, errorobj);
			}
		}


		[HttpPost]
		public async Task<ActionResult> SetCurrentMenu([FromBody] IEnumerable<int> recepieIds)
		{
			try
			{
				var currentMenu = await Repository.FindAsync(r => r.IsOnMenu);
				currentMenu.All(r => { r.IsOnMenu = false; return true; });
				
				var newMenu = await Repository.FindAsync(r => recepieIds.Contains(r.Id));
				newMenu.All(r => { r.IsOnMenu= true; return true; });

				await Repository.CommitAsync();
				return StatusCode(StatusCodes.Status501NotImplemented);
			}
			catch (Exception ex)
			{
				var methodInfo = MethodBase.GetCurrentMethod();
				Logger.LogError(ex, $"An Exception occured in {methodInfo.DeclaringType.Name}.{methodInfo.Name}");
				var errorobj = new { Error = ex.GetType().Name, ex.Message };
				await Repository.RevertAsync();
				return StatusCode(StatusCodes.Status500InternalServerError, errorobj);
			}
		}

	}
}
