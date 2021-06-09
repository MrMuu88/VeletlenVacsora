using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeletlenVacsora.Api.ViewModels;
using VeletlenVacsora.Data;
using VeletlenVacsora.Data.Models;

namespace VeletlenVacsora.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[ProducesErrorResponseType(typeof(ExceptionResponse))]
	public class RecepiesController : ControllerBase
	{
		public VacsoraDbContext DbContext { get; }
		public IMapper Mapper { get; }

		public RecepiesController(VacsoraDbContext dbContext, IMapper mapper)
		{
			DbContext = dbContext;
			Mapper = mapper;
		}


		[HttpPost]
		[ProducesResponseType(statusCode: 201, type: typeof(RecepieResponse))]
		public async Task<ActionResult> Create([FromBody] RecepieRequest model)
		{
			var entity = Mapper.Map<Recepie>(model);
			await DbContext.AddAsync(entity);
			await DbContext.SaveChangesAsync();

			var response = Mapper.Map<RecepieResponse>(entity);
			return CreatedAtAction(nameof(Create), response);
		}

		[HttpGet("List")]
		[ProducesResponseType(statusCode: 200, type: typeof(int[]))]
		public async Task<ActionResult<IEnumerable<int>>> List()
		{
			var ids = await DbContext.Recepies.Select(r => r.id).ToArrayAsync();
			return Ok(ids);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(statusCode: 404)]
		[ProducesResponseType(statusCode: 200, type: typeof(RecepieResponse))]
		public async Task<ActionResult<RecepieResponse>> Get(int id)
		{
			var entity = await DbContext.Recepies.FirstOrDefaultAsync(r => r.id == id);

			if (entity == null) return NotFound();

			var response = Mapper.Map<RecepieResponse>(entity);
			return Ok(response);
		}


		[HttpGet]
		[ProducesResponseType(statusCode: 400)]
		[ProducesResponseType(statusCode: 200, type: typeof(RecepieResponse))]
		public async Task<ActionResult<IEnumerable<RecepieResponse>>> GetMany([FromQuery] string ids)
		{
			int[] idArray;
			
			try 
			{
				idArray = ids.Split(',').Select(id => int.Parse(id)).ToArray();
			}
			catch {
				return BadRequest();
			}

			var entity = await DbContext.Recepies.Where(r => idArray.Contains(r.id)).ToListAsync();

			var response = Mapper.Map<List<RecepieResponse>>(entity);
			return Ok(response);

		}

		[HttpPut("{id}")]
		[ProducesResponseType(statusCode: 404)]
		[ProducesResponseType(statusCode: 200, type: typeof(RecepieResponse))]
		public async Task<ActionResult> Update([FromRoute]int id,[FromBody] RecepieRequest model)
		{
			var entity = await DbContext.Recepies.FirstOrDefaultAsync(r => r.id == id);

			if (entity == null) return NotFound();

			entity = Mapper.Map(model, entity);

			DbContext.Update(entity);
			await DbContext.SaveChangesAsync();

			var response = Mapper.Map<RecepieResponse>(entity);

			return Ok(response);
		}


		[HttpDelete("{id}")]
		[ProducesResponseType(statusCode: 404)]
		public async Task<ActionResult> Delete(int id)
		{
			var entity = await DbContext.Recepies.FirstOrDefaultAsync(r => r.id == id);
			
			if (entity == null) return NotFound();

			DbContext.Remove(entity);
			await DbContext.SaveChangesAsync();
			return Ok();
		}


	}
}
