using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VeletlenVacsora.Data.Extensions;
using VeletlenVacsora.Data.Models;
using VeletlenVacsora.Data.Repositories;

namespace VeletlenVacsora.Api.Services
{
	public class WeightManagerService : IHostedService, IDisposable
	{
		public ILogger<WeightManagerService> Logger { get; }
		public IServiceProvider Services { get; }
		public Timer Timer { get; private set; }

		public WeightManagerService(ILogger<WeightManagerService> logger,IServiceProvider services)
		{
			Logger = logger;
			Services = services;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			Logger.LogInformation("Starting Weight Manager service");
			//TODO fire Callback at exact Time from config
			Timer = new Timer(DoWork, null, 0, (int)TimeSpan.FromDays(1).TotalMilliseconds);
			return Task.CompletedTask;
		}

		private async void DoWork(object state)
		{
			//Async void is a fire and forget Task, and all exceptions has to be handled
			try
			{
				Logger.LogInformation("Executing daily Weight raise");
				using var scope = Services.CreateScope();
				var Repo = scope.ServiceProvider.GetService<IRepository<RecepieModel>>();
				var affected = await Repo.RaiseWeightsAsync();
				Logger.LogInformation($"Weight raised on {affected} Recepies");
			}
			catch (Exception ex) {
				Logger.LogError(ex, "an Exception occured when Raising Recepie Weights");
			}
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			Logger.LogInformation("Stoping Weight Manager service");
			Timer.Change(Timeout.Infinite, 0);
			return Task.CompletedTask;
		}
		public void Dispose()
		{
			Logger.LogTrace("Disposing WeightManager");
			Timer?.Dispose();
		}
	}
}
