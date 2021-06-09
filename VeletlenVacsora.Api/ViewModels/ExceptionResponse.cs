using System;
using System.Diagnostics;

namespace VeletlenVacsora.Api.ViewModels
{
	public class ExceptionResponse
	{
		public string Exception { get; init; }
		public string Message { get; init; }

		public ExceptionResponse(Exception ex)
		{
			Exception = ex.GetType().Name;
			Message = ex.Message;
		}
	}
}
