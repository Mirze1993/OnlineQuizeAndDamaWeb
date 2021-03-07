using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DamaWeb.Tools.BackGroundTask
{
    public class ConsumeScopedServiceHostedService : BackgroundService
    {
        //private readonly ILogger<ConsumeScopedServiceHostedService> logger;
        //public IServiceProvider Services { get; }

       
        private Timer timer;
        protected override  Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //await DoWork(stoppingToken);
            timer = new Timer(DoWork, null, TimeSpan.FromSeconds(1), TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }
        private void DoWork(object state)
        {
            OnlineUsers.RemoveUsers(); 
        }
        //private async Task DoWork(CancellationToken stoppingToken)
        //{
        //    logger.LogError("Consume Scoped Service Hosted Service is working.");
        //    using (var scope= Services.CreateScope())
        //    {
        //        var pros=scope.ServiceProvider.d GetRequiredService<IScopedProcessingService>();
        //        await pros.DoWork(stoppingToken);
        //    }
        //}

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            await base.StopAsync(cancellationToken);
        }
        public override void Dispose()
        {
            timer?.Dispose();
            base.Dispose();
        }
    }
}
