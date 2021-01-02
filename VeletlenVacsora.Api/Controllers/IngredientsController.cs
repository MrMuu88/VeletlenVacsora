using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using VeletlenVacsora.Api.ViewModels;
using VeletlenVacsora.Data.Models;
using VeletlenVacsora.Data.Repositories;
using VeletlenVacsora.Data.Extensions;
using System;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace VeletlenVacsora.Api.Controllers
{
	[ApiController]
	[Route("Api/[Controller]/[Action]")]
	public class IngredientsController : DefaultCRUDController<IngredientModel,Ingredient>
	{
		public IRepository<CategoryModel> CategoryRepo { get; }
		public IngredientsController(ILogger<IngredientsController> logger,IRepository<IngredientModel> repo,IRepository<CategoryModel> CategoryRepo, IMapper mapper) : base(logger,repo,mapper)
		{
			this.CategoryRepo = CategoryRepo;
		}

		public override async Task<ActionResult> Create([FromBody] Ingredient model)
		{
			try
			{
				var entity = Mapper.Map<IngredientModel>(model);
				entity.IngredientType = await CategoryRepo.UpsertByNameAsync(model.IngredientType,CategoryType.Ingredient);
				entity.PackageType = await CategoryRepo.UpsertByNameAsync(model.PackageType,CategoryType.Package);
				await Repository.AddAsync(entity);
				await Repository.CommitAsync();
				model = Mapper.Map<Ingredient>(entity);
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
