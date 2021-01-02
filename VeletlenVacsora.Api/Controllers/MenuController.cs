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
using System.ComponentModel.DataAnnotations;

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

		/// <summary>
		///Returns the currently Set Menu 
		/// </summary>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<Recepie>),200)]
		[ProducesResponseType(500)]
		public async Task<ActionResult<IEnumerable<Recepie>>> GetCurrentMenu()
		{
			try
			{
				var menu = await Repository.FindAsync(r => r.OnMenu != null);
				var vm = Mapper.Map<IEnumerable<Recepie>>(menu.OrderBy(r => r.OnMenu));
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

		/// <summary>
		/// Set An Entire Menu by recepieId list. the order of the menu wil be the order of the Ids.
		/// </summary>
		/// <param name="recepieIds">the List of recepieIds</param>
		/// <returns></returns>

		[HttpPost]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		public async Task<ActionResult> SetCurrentMenu([FromBody] int[] recepieIds)
		{
			try
			{
				var currentMenu = await Repository.FindAsync(r => r.OnMenu != null);
				currentMenu.All(r => { r.OnMenu = null; return true; });
				
				var newMenu = await Repository.FindAsync(r => recepieIds.Contains(r.Id));
				if (newMenu.Count != recepieIds.Length)
					return BadRequest("some of the provided recepie Ids are invalid");
				
				for (int i = 0; i < recepieIds.Length; i++)
				{
					var recepie = newMenu.First(r => r.Id == recepieIds[i]);
					recepie.OnMenu = i + 1;
					recepie.Weight = 0;
				}

				await Repository.CommitAsync();
				return Ok();
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

		/// <summary>
		/// Set a Recepie to the Menu Given order. if another recepi is on that order, it will be overwriten
		/// </summary>
		/// <param name="order">the menuorder to set to</param>
		/// <param name="recepieId">the recepie Id</param>
		/// <returns></returns>
		[HttpPost]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		public async Task<ActionResult> SetRecepieOnMenu([FromQuery][Required]int order, [FromQuery][Required] int recepieId)
		{
			try
			{
				var currentMenuItem = (await Repository.FindAsync(r => r.OnMenu == order)).FirstOrDefault();
				if(currentMenuItem != null)
					currentMenuItem.OnMenu = null;
				var newMenuItem = await Repository.GetAsync(recepieId);
				if (newMenuItem == null) {
					return BadRequest($"Recepie with Id:{recepieId} does not exists");
				}
				newMenuItem.OnMenu = order;
				newMenuItem.Weight = 0;

				await Repository.CommitAsync();

				return Ok();
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
