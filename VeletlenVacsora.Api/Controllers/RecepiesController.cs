using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeletlenVacsora.Api.ViewModels;
using VeletlenVacsora.Data;

namespace VeletlenVacsora.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[ProducesErrorResponseType(typeof(ExceptionResponse))]
	public class RecepiesController : ControllerBase
	{
		public VacsoraDbContext DbContext { get; }
		public RecepiesController(VacsoraDbContext dbContext)
		{
			DbContext = dbContext;
		}


		[HttpPost]
		public async Task<ActionResult> Create([FromBody] RecepieRequest model)
		{
			//TODO Implement
			throw new NotImplementedException();
		}

		[HttpGet("List")]
		public async Task<ActionResult<IEnumerable<int>>> List()
		{
			//TODO Implement
			throw new NotImplementedException();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<IEnumerable<int>>> Get(int id)
		{
			//TODO Implement
			throw new NotImplementedException();
		}


		[HttpGet]
		public async Task<ActionResult<IEnumerable<int>>> GetMany([FromQuery]string Ids)
		{
			//TODO Implement
			throw new NotImplementedException();
		}

		[HttpPut]
		public async Task<ActionResult> Update([FromBody] RecepieRequest model)
		{
			//TODO Implement
			throw new NotImplementedException();
		}


		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			//TODO Implement
			throw new NotImplementedException();
		}


	}
}
