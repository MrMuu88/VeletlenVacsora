using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeletlenVacsora.Data.Models;
using VeletlenVacsora.Data.Repositories;

namespace VeletlenVacsora.Api.Controllers
{
	public class BaseModelController<TEntity,TMap> : ControllerBase where TEntity : BaseModel where TMap:class
	{
		protected readonly ILogger<BaseModelController<TEntity, TMap>> logger;
		internal IMapper Mapper { get; set; }
		internal IRepository<TEntity> Repository { get; set; }
		public BaseModelController(ILogger<BaseModelController<TEntity, TMap>> logger ,IRepository<TEntity> repo, IMapper mapper)
		{
			this.logger = logger;
			Repository = repo;
			Mapper = mapper;
		}

		/// <summary>
		/// This method returns a list of entities from the Database, according to the requested Ids.
		/// If no Ids are provided, returns all entries.
		/// if not all Ids are found in the DB, returns http 206 (PartialContent)
		/// </summary>
		[HttpPost]
		[ProducesResponseType(200)]
		[ProducesResponseType(206)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public virtual async Task<ActionResult<IEnumerable<TMap>>> GetMany(IEnumerable<int> ids = null)
		{
			try
			{
				var entities = await Repository.GetManyAsync(ids);
				var mapped = Mapper.Map<IEnumerable<TMap>>(entities);

				if (ids != null && ids.Count() != entities.Count)
					return StatusCode(206, mapped);
				else
					return Ok(mapped);
			}
			catch (Exception ex)
			{
				logger.LogError(ex,$"An execption occured in {nameof(TMap)}Controller.{nameof(GetMany)} method");
				var errorobj = new { Error = ex.GetType().Name, ex.Message };
				return StatusCode(StatusCodes.Status500InternalServerError,errorobj);
			}
		}

		/// <summary>
		/// Returns a List of all entity Ids from the Database
		/// </summary>
		[HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(500)]

		public async Task<ActionResult<IEnumerable<int>>> List()
		{
			try
			{
				var ids = await Repository.ListAsync();
				return Ok(ids);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"An execption occured in {nameof(TMap)}Controller.{nameof(GetMany)} method");
				var errorobj = new { Error = ex.GetType().Name, ex.Message };
				return StatusCode(StatusCodes.Status500InternalServerError, errorobj);
			}
		}


		/// <summary>
		/// Returns the entity with the given Id from the database
		/// </summary>
		/// <param name="id">the database Id</param>
		[HttpGet("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public virtual async Task<ActionResult<TMap>> GetById(int id)
		{
			try
			{
				var entity = await Repository.GetAsync(id);
				if (entity == null)
					return NotFound();
				var mapped = Mapper.Map<TMap>(entity);
				return Ok(mapped);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"An execption occured in {nameof(TMap)}Controller.{nameof(GetById)} method",id);
				var errorobj = new { Error = ex.GetType().Name, ex.Message };
				return StatusCode(StatusCodes.Status500InternalServerError, errorobj);
			}
		}
		/// <summary>
		/// Returns an int indication how many Entites are stored in the database
		/// </summary>
		[HttpGet]
		[ProducesResponseType(typeof(int),200)]
		[ProducesResponseType(500)]
		public async Task<ActionResult<int>> Count()
		{
			try
			{
				return Ok(await Repository.CountAsync());
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"An execption occured in {nameof(TMap)}Controller.{nameof(Count)} method");
				var errorobj = new { Error = ex.GetType().Name, ex.Message };
				return StatusCode(StatusCodes.Status500InternalServerError, errorobj);
			}
		}

		/// <summary>
		/// Creates a new Entity in the Database from the provided body
		/// </summary>
		/// <param name="model"> the entity to be created</param>
		/// <returns></returns>
		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(500)]
		public async Task<ActionResult> Create([FromBody] TMap model)
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
				logger.LogError(ex, $"An execption occured in {nameof(TMap)}Controller.{nameof(Create)} method", model);
				await Repository.RevertAsync();
				var errorobj = new { Error = ex.GetType().Name, ex.Message };
				return StatusCode(StatusCodes.Status500InternalServerError, errorobj);
			}
		}

		/// <summary>
		/// Deletes the entity with the Given Id from The Database
		/// </summary>
		/// <param name="id">the Id of the entity to Delete</param>
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public async Task<ActionResult> Delete([FromRoute]int id)
		{
			try
			{
				var entity = await Repository.GetAsync(id);
				if (entity == null)
					return NotFound();
				await Repository.DeleteAsync(entity);
				await Repository.CommitAsync();
				return Ok(entity);
			}
			catch (Exception ex)
			{
				await Repository.RevertAsync();
				logger.LogError(ex, $"An execption occured in {nameof(TMap)}Controller.{nameof(Delete)} method",id);
				var errorobj = new { Error = ex.GetType().Name, ex.Message };
				return StatusCode(StatusCodes.Status500InternalServerError, errorobj);
			}
		}

		/// <summary>
		/// Upserts the Entity in the Database with the given Id
		/// </summary>
		/// <param name="id">the id of the Entity to be upserted</param>
		/// <param name="model"> the entity to be created</param>
		/// <returns></returns>
		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(201)]
		[ProducesResponseType(500)]
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
				logger.LogError(ex, $"An execption occured in {nameof(TMap)}Controller.{nameof(Update)} method",id,model);
				var errorobj = new { Error = ex.GetType().Name, ex.Message };
				return StatusCode(StatusCodes.Status500InternalServerError, errorobj);
			}
		}

	}
}
