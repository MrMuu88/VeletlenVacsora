using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VeletlenVacsora.Api.ViewModels;
using VeletlenVacsora.Data.Models;
using VeletlenVacsora.Data.Repositories;

namespace VeletlenVacsora.Api.Controllers
{
	[ApiController]
	[Route("Api/[Controller]")]
	public class RecepiesController : BaseModelController<RecepieModel,Recepie>
	{
		public RecepiesController(IRepository<RecepieModel> repo, IMapper mapper) : base(repo, mapper)
		{
		}
	}
}
