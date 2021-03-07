using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DamaWeb.Tools
{
    public class TimeHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<TimeHostedService> logger;
        private int executionCount = 0;
        private Timer timer;

        public TimeHostedService(ILogger<TimeHostedService> logger)
        {
            this.logger = logger;
        }
        public void Dispose()
        {
            timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogError("Timer start");
            timer = new Timer(DoWork, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var count=Interlocked.Increment(ref executionCount);
            logger.LogError(
            "Timed Hosted Service is working. Count: {Count}", count);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogError("Timed Hosted Service is stopping.");
            timer?.Change(Timeout.Infinite, 0);
            return  Task.CompletedTask;
        }
    }
}
