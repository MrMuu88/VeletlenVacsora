using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VeletlenVacsora.Api.ViewModels;
using VeletlenVacsora.Data.Models;
using VeletlenVacsora.Data.Repositories;

namespace VeletlenVacsora.Api.Controllers
{
	[ApiController]
	[Route("Api/[Controller]")]
	public class RecepiesController : BaseModelController<RecepieModel,Recepie>
	{
		public RecepiesController(ILogger<RecepiesController> logger,IRepository<RecepieModel> repo, IMapper mapper) : base(logger,repo, mapper)
		{
		}
	}
}
