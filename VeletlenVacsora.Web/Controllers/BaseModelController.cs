using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeletlenVacsora.Data.Models;
using VeletlenVacsora.Data.Repositories;

namespace VeletlenVacsora.Web.Controllers
{
	public class BaseModelController<T> : ControllerBase where T : BaseModel
	{
		internal IRepository<T> Repository { get; set; }
		public BaseModelController(IRepository<T> repo)
		{
			Repository = repo;
		}

		/// <summary>
		/// This method return all entities
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(401)]
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
		
		[HttpGet("{id}")]
		public virtual async Task<ActionResult<T>> GetById(int id)
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

		[HttpGet("count")]
		public async Task<ActionResult<int>> Count()
		{
			try
			{
				return Ok(await Repository.CountAsync());
			}
			catch (Exception ex)
			{

				var errorobj = new { Error = ex.GetType().Name, ex.Message };
				return StatusCode(StatusCodes.Status500InternalServerError, errorobj);
			}
		}

		[HttpPost]
		public async Task<ActionResult> Add([FromBody]T model)
		{
			try
			{
				await Repository.AddAsync(model);
				await Repository.CommitAsync();
				return Created(new Uri($"{Request.Path}/{model.Id}"), model);
			}
			catch (Exception ex)
			{
				await Repository.RevertAsync();
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


		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id,[FromBody]T model)
		{
			try
			{
				if (await Repository.Exist(id))
				{
					model.Id = id;
					await Repository.UpdateAsync(model);
				}
				else {
					await Repository.AddAsync(model);
				}
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
