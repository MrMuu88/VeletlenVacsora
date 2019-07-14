using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace VeletlenVacsora.Web {
	public class Program {
		public static void Main(string[] args) {
			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseKestrel()
				.UseUrls("http://0.0.0.0:1234")
				.UseStartup<Startup>();
	}
}
