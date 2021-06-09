using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace VeletlenVacsora.Api.Middlewares
{
	public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = 500;
                await httpContext.Response.WriteAsJsonAsync(new
                {
                    Exception = ex.GetType().Name,
                    ThrownFrom = ex.Source,
                    Message = ex.Message,
                });
            }
        }
       
    }
}
