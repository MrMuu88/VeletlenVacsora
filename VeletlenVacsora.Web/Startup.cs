using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using VeletlenVacsora.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace VeletlenVacsora.Web
{
	public class Startup {
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration config) {
			Configuration = config;
		}

		public void ConfigureServices(IServiceCollection services) {

			services.AddControllers();
			services.AddSpaStaticFiles(configuration => configuration.RootPath="VeletlenVacsoraApp/dist");

			services.AddDbContext<VacsoraDBContext>(options => options.UseSqlite(Configuration.GetConnectionString("VacsoraDB")));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}
			app.UseStaticFiles();
			if (!env.IsDevelopment()) {
				app.UseSpaStaticFiles();
			}

			app.UseRouting();

			app.UseEndpoints(endpoints=>
				{
					endpoints.MapControllerRoute(name:"default",pattern:"{controller}/{action=Index}/{id?}");
				}
			);

			app.UseSpa(spa =>
			{
				// To learn more about options for serving an Angular SPA from ASP.NET Core,
				// see https://go.microsoft.com/fwlink/?linkid=864501
				spa.Options.SourcePath = "VeletlenVacsoraApp";

				if (env.IsDevelopment())
				{
					spa.UseAngularCliServer(npmScript: "start");
				}
			});
		}
	}
}
