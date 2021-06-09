using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using VeletlenVacsora.Data;
using Microsoft.EntityFrameworkCore;
using VeletlenVacsora.Api.Middlewares;
using VeletlenVacsora.Api.MappingProfiles;

namespace VeletlenVacsora.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "VeletlenVacsora.Api", Version = "v1" });
			});

			services.AddDbContext<VacsoraDbContext>(o => o.UseSqlite(Configuration.GetConnectionString("VacsoraDB")));
			services.AddAutoMapper(typeof(RecepiesMapping));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VeletlenVacsora.Api v1"));
			}

			app.UseMiddleware<ExceptionMiddleware>();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
