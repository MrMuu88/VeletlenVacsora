using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;
using VeletlenVacsora.Api.ViewModels;
using VeletlenVacsora.Data.Extensions;
using VeletlenVacsora.Data.Models;
using VeletlenVacsora.Data.Repositories;

namespace VeletlenVacsora.Api.Controllers
{
	[ApiController]
	[Route("Api/[Controller]/[Action]")]
	public class RecepiesController : BaseModelController<RecepieModel,Recepie>
	{
		public RecepiesController(ILogger<RecepiesController> logger,IRepository<RecepieModel> repo, IMapper mapper) : base(logger,repo, mapper)
		{
		}

		/// <summary>
		/// Returns a Recepie based on Weightened random selection
		/// </summary>
		[HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(500)]
		public async Task<ActionResult<Recepie>> GetRandom()
		{
			try
			{
				var recepie = await Repository.GetRandomAsync();
				return Ok(recepie);

			}
			catch (Exception ex)
			{
				var methodInfo = MethodBase.GetCurrentMethod();
				logger.LogError(ex, $"An Exception occured in {methodInfo.DeclaringType.Name}.{methodInfo.Name}");
				var errorobj = new { Error = ex.GetType().Name, ex.Message };
				return StatusCode(StatusCodes.Status500InternalServerError, errorobj);
			}
		}


	}
}
