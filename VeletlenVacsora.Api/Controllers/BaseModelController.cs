﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeletlenVacsora.Data.Models;
using VeletlenVacsora.Data.Repositories;

namespace VeletlenVacsora.Api.Controllers
{
	public class BaseModelController<TEntity,TMap> : ControllerBase where TEntity : BaseModel
	{
		internal IMapper Mapper { get; set; }
		internal IRepository<TEntity> Repository { get; set; }
		public BaseModelController(IRepository<TEntity> repo, IMapper mapper)
		{
			Repository = repo;
		}

		/// <summary>
		/// This method return all entities
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[ProducesResponseType(200)]
		public virtual async Task<ActionResult<IEnumerable<TMap>>> GetAll()
		{
			try
			{
				var entities = await Repository.GetAllAsync();
				var mapped = Mapper.Map<IEnumerable<TMap>>(entities);
				
				return Ok(mapped);
			}
			catch (Exception ex)
			{
				var errorobj = new { Error = ex.GetType().Name, ex.Message };
				return StatusCode(StatusCodes.Status500InternalServerError,errorobj);
			}
		}
		
		[HttpGet("{id}")]
		public virtual async Task<ActionResult<TMap>> GetById(int id)
		{
			try
			{
				var entity = await Repository.GetAsync(id);
				var mapped = Mapper.Map<TMap>(entity);
				return Ok(mapped);
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
		public async Task<ActionResult> Add([FromBody] TMap model)
		{
			try
			{
				var entity = Mapper.Map<TEntity>(model);
				await Repository.AddAsync(entity);
				await Repository.CommitAsync();
				return Created(new Uri($"{Request.Path}/{entity.Id}"), entity);
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
		public async Task<ActionResult> Update(int id,[FromBody] TMap model)
		{
			try
			{
				var entity = Mapper.Map<TEntity>(model);
				if (await Repository.Exist(id))
				{
					entity.Id = id;
					await Repository.UpdateAsync(entity);
				}
				else {
					await Repository.AddAsync(entity);
				}
				await Repository.CommitAsync();
				return Ok(new Uri($"{Request.Path}/{entity.Id}"));
			}
			catch (Exception ex)
			{
				var errorobj = new { Error = ex.GetType().Name, ex.Message };
				return StatusCode(StatusCodes.Status500InternalServerError, errorobj);
			}
		}

	}
}
