using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VeletlenVacsora.Api.ViewModels;
using VeletlenVacsora.Data.Models;
using VeletlenVacsora.Data.Repositories;

namespace VeletlenVacsora.Api.Controllers
{
	[ApiController]
	[Route("Api/[Controller]")]
	public class IngredientsController : BaseModelController<IngredientModel,Ingredient>
	{
		public IngredientsController(IRepository<IngredientModel> repo, IMapper mapper) : base(repo,mapper)
		{
		}
	}
}
