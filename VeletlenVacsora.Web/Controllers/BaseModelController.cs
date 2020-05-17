using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeletlenVacsora.Data.Models;
using VeletlenVacsora.Data.Repositories;

namespace VeletlenVacsora.Web.Controllers
{
	public class BaseModelController<T>:ControllerBase where T:BaseModel
	{
		internal IRepository<T> Repository { get; set; }
		public BaseModelController(IRepository<T> repo)
		{
			Repository = repo;
		}

		[HttpGet]
		public virtual async Task<ActionResult<IEnumerable<T>>> GetAll()
		{
			try
			{
				var entities = await Repository.GetAllAsync();
				return Ok(entities);
			}
			catch (Exception ex)
			{
				var errorobj = new { Error = ex.GetType().Name, ex.Message };
				return StatusCode(StatusCodes.Status500InternalServerError,errorobj);
			}
		}

		[HttpGet("{Id}")]
		public virtual async Task<ActionResult<T>> GetById([FromRoute]int id)
		{
			try
			{
				var entitie = await Repository.GetAsync(id);
				return Ok(entitie);
			}
			catch (Exception ex)
			{
				var errorobj = new { Error = ex.GetType().Name, ex.Message };
				return StatusCode(StatusCodes.Status500InternalServerError, errorobj);
			}
		}

		[HttpGet]
		public async Task<ActionResult<int>> Count()
		{
			try
			{
				return Ok(await Repository.CountAsync());
			}
			catch (Exception ex)
			{

				var errorobj = new { Error = ex.GetType().Name, ex.Message };
				return StatusCode(StatusCodes.Status500InternalServerError,errorobj);
			}
		}

		[HttpPost]
		public async Task<ActionResult> Add([FromBody]T model)
		{
			try
			{
				await Repository.AddAsync(model);
				await Repository.CommitAsync();
				return Created(new Uri($"{Request.Path}/{model.Id}"),model);
			}
			catch (Exception ex)
			{
				await Repository.RevertAsync();
				var errorobj = new { Error = ex.GetType().Name, ex.Message };
				return StatusCode(StatusCodes.Status500InternalServerError, errorobj);
			}
		}

		[HttpPost]
		public async Task<ActionResult> AddRange([FromBody]IEnumerable<T> models)
		{
			try
			{
				await Repository.AddRangeAsync(models);
				await Repository.CommitAsync();
				return Created(new Uri($"{Request.Path}"), models);
			}
			catch (Exception ex)
			{
				var errorobj = new { Error = ex.GetType().Name, ex.Message };
				return StatusCode(StatusCodes.Status500InternalServerError, errorobj);
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete([FromRoute]int id)
		{
			try
			{
				var entity = await Repository.GetAsync(id);
				await Repository.DeleteAsync(entity);
				await Repository.CommitAsync();
				return Ok(entity);
			}
			catch (Exception ex)
			{
				await Repository.RevertAsync();
				var errorobj = new { Error = ex.GetType().Name, ex.Message };
				return StatusCode(StatusCodes.Status500InternalServerError, errorobj);
			}
		}


		[HttpPut]
		public async Task<ActionResult> Update([FromBody]T model)
		{
			try
			{
				await Repository.UpdateAsync(model);
				await Repository.CommitAsync();
				return Ok(model);
			}
			catch (Exception ex)
			{
				var errorobj = new { Error = ex.GetType().Name, ex.Message };
				return StatusCode(StatusCodes.Status500InternalServerError, errorobj);
			}
		}

	}
}
