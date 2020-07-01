using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SIXTReservationApp.BackgroundServices
{
    internal class SIXTCancellationBackgroundHostedService : BackgroundService
    {
     //   private readonly IServiceProvider ServiceProvider;
        private readonly IServiceScopeFactory ServiceProvider;
        // private readonly TimeSpan ExecuteInterval = TimeSpan.FromMinutes(1);
        private DateTime lastRun;

        public SIXTCancellationBackgroundHostedService(IServiceScopeFactory sp)
        {
            ServiceProvider = sp;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await StartWorker(stoppingToken);
        }

        private async Task StartWorker(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = ServiceProvider.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<IPushNotificationService>();
                    var now = DateTime.Now;
                    //if (lastRun == DateTime.MinValue)
                    //{
                    //    lastRun = now.Add(-ExecuteInterval);
                    //}
                    await service.DoWork();
                    // lastRun = now;
                }
                // await Task.Delay(ExecuteInterval);
            }
        }
    }
}


