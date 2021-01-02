using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VeletlenVacsora.Api.ViewModels;
using VeletlenVacsora.Data.Models;
using VeletlenVacsora.Data.Repositories;

namespace VeletlenVacsora.Api.Controllers
{
	[ApiController]
	[Route("Api/[Controller]/[Action]")]
	public class CategoriesController : DefaultCRUDController<CategoryModel,Category>
	{
		public CategoriesController(ILogger<CategoriesController> logger, IRepository<CategoryModel> repo, IMapper mapper) : base(logger, repo, mapper)
		{
		}
	}
}
