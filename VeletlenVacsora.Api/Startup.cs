using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using VeletlenVacsora.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VeletlenVacsora.Data.Repositories;
using Microsoft.OpenApi.Models;
using VeletlenVacsora.Api.Configurations;
using VeletlenVacsora.Data.Models;
using AutoMapper;
using VeletlenVacsora.Api.MapperProfiles;

namespace VeletlenVacsora.Api
{
	public class Startup {
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration config) {
			Configuration = config;
		}

		public void ConfigureServices(IServiceCollection services) {

			services.AddControllers();

			services.AddSwaggerGen(c => {
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Véletlen Vacsora Api", Version = "v1" });
				c.SchemaFilter<EnumSchemaFilter>();
				c.IncludeXmlComments(@".\VeletlenVacsora.Api.xml");
			});

			services.AddAutoMapper(typeof(MapingProfile));

			services.AddDbContext<VacsoraDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("VacsoraDB")));

			services.AddScoped<IRepository<RecepieModel>,BaseModelRepository<RecepieModel>>();
			services.AddScoped<IRepository<IngredientModel>,BaseModelRepository<IngredientModel>>();
			services.AddScoped<IRepository<CategoryModel>,BaseModelRepository<CategoryModel>>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseSwagger(a => a.RouteTemplate = "/api/docs/{documentName}.json");

			app.UseSwaggerUI(c =>{
				c.SwaggerEndpoint("/api/docs/v1.json", "Véletlen Vacsora Api v1");
			});
			app.UseRouting();

			app.UseEndpoints(endpoints=>
				{
					endpoints.MapControllerRoute(name:"default",pattern:"{controller}/{action=Index}/{id?}");
				}
			);

			
		}
	}
}
