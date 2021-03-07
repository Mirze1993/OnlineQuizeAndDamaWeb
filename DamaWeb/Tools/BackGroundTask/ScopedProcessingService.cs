using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DamaWeb.Tools.BackGroundTask
{
    internal class ScopedProcessingService : IScopedProcessingService
    {
        private readonly ILogger<ScopedProcessingService> logger;
        private int executionCount = 0;

        public ScopedProcessingService(ILogger<ScopedProcessingService> logger)
        {
            this.logger = logger;
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
           
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;
                logger.LogError("Scoped Processing Service is working. Count: {Count}", executionCount);
                await Task.Delay(2000, stoppingToken);
                if (executionCount == 5) return;
            }
            
        }
    }
}
