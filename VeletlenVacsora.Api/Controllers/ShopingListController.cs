using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VeletlenVacsora.Api.ViewModels;
using VeletlenVacsora.Data.Models;
using VeletlenVacsora.Data.Repositories;

namespace VeletlenVacsora.Api.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ShopingListController : ControllerBase
	{

		public ILogger Logger { get; }
		public IRepository<RecepieModel> Repository { get; }
		public IMapper Mapper { get; }
		public ShopingListController(ILogger logger, IRepository<RecepieModel> repository,IMapper mapper)
		{
			Logger = logger;
			Repository = repository;
			Mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<RecepieIngredient>),200)]
		[ProducesResponseType(500)]
		public async Task<ActionResult<IEnumerable<RecepieIngredient>>> GetCurrentShopingList()
		{
			try
			{
				var currentmenu = await Repository.FindAsync(r => r.OnMenu != null);
				Mapper.Map<IEnumerable<RecepieIngredient>>(currentmenu.SelectMany(r => r.Ingredients).ToArray());
				return StatusCode(StatusCodes.Status501NotImplemented);
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
