using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace VeletlenVacsora.Api {
	public class Program {
		public static void Main(string[] args) {
			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseIIS()
				.UseSerilog((context,Config)=>Config.ReadFrom.Configuration(context.Configuration,"Serilog"))
				.UseStartup<Startup>();
	}
}
