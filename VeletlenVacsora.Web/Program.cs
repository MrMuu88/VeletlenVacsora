using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace VeletlenVacsora.Api {
	public class Program {
		public static void Main(string[] args) {
			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseIIS()
				//.UseKestrel()
				//.UseUrls("http://0.0.0.0:1234")
				.UseStartup<Startup>();
	}
}
