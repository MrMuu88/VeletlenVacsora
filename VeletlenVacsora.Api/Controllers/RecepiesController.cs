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
	public class RecepiesController : DefaultCRUDController<RecepieModel,Recepie>
	{
		public IRepository<CategoryModel> CategoryRepository { get; }
		public RecepiesController(ILogger<RecepiesController> logger,IRepository<RecepieModel> repo,IRepository<CategoryModel> categoryRepository, IMapper mapper) : base(logger,repo, mapper)
		{
			CategoryRepository = categoryRepository;
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
				var vm = Mapper.Map<Recepie>(recepie);
				return Ok(vm);

			}
			catch (Exception ex)
			{
				var methodInfo = MethodBase.GetCurrentMethod();
				logger.LogError(ex, $"An Exception occured in {methodInfo.DeclaringType.Name}.{methodInfo.Name}");
				var errorobj = new { Error = ex.GetType().Name, ex.Message };
				return StatusCode(StatusCodes.Status500InternalServerError, errorobj);
			}
		}

		public override async  Task<ActionResult> Create([FromBody] Recepie model)
		{
			try
			{
				var entity = Mapper.Map<RecepieModel>(model);
				entity.Category = await CategoryRepository.UpsertByNameAsync(model.Category,CategoryType.Recepie);
				await Repository.AddAsync(entity);
				await Repository.CommitAsync();
				model = Mapper.Map<Recepie>(entity);
				return Created(this.Url.Action(nameof(GetById), new { id = entity.Id }), model);
			}
			catch (Exception ex)
			{
				var methodInfo = MethodBase.GetCurrentMethod();
				logger.LogError(ex, $"An Exception occured in {methodInfo.DeclaringType.Name}.{methodInfo.Name}");
				await Repository.RevertAsync();
				var errorobj = new { Error = ex.GetType().Name, ex.Message };
				return StatusCode(StatusCodes.Status500InternalServerError, errorobj);
			}
		}


	}
}
