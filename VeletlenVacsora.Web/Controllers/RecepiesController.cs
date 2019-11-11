﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeletlenVacsora.Web.Services;
using VeletlenVacsora.Web.Models;

namespace VeletlenVacsora.Web.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class RecepiesController : ControllerBase {
		private IVacsoraRepository _repository;

		//TODO Implement Post Method
		//TODO Implement Put Method
		//TODO Implement Delete Method

		public RecepiesController(IVacsoraRepository repository) {
			_repository = repository;
		}


		[HttpGet]
		public async Task<ActionResult<ICollection<RecepieModel>>> GetRecepies(string Category = "") {
			try {
				var results = new List<RecepieModel>();
				if (!string.IsNullOrWhiteSpace(Category)) {
					var raw = await _repository.GetRecepiesByTypeAsync(Category);

					foreach (var r in raw) {
						results.Add(new RecepieModel(r));
					}
					return Ok(results);
				} else {
					foreach (var recepie in await _repository.GetAllRecepiesAsync()) {
						results.Add(new RecepieModel(recepie));
					}
					return Ok(results);
				}

			} catch (Exception ex) {
				return StatusCode(StatusCodes.Status500InternalServerError, $"Server Failure: {ex.GetType().Name}\n{ex.Message}");
			}
		}




		[HttpGet("{ID}")]
		public async Task<ActionResult<RecepieModel>> GetRecepieByID(int ID) {
			try {
				var result = await _repository.GetRecepieByIDAsync(ID);

				return Ok(new RecepieModel(result));

			} catch (Exception ex) {
				return StatusCode(StatusCodes.Status500InternalServerError, $"Server Failure: {ex.GetType().Name}\n{ex.Message}");
			}
		}
	}
}