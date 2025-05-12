using Platform.Entity.Interfaces;
using Platform.Transactional.Operations.WorkerOperations;

namespace ProcessRegisterClientBatch
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;


        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var batchClientRegisterOperations = scope.ServiceProvider.GetRequiredService<IBatchClientRegisterOperations>();
                    var clientOperations = scope.ServiceProvider.GetRequiredService<IClientOperations>();
                    new ProcessClientRegisterOperations(batchClientRegisterOperations, clientOperations).ProcessRegisterClientBatch();
                }

                _logger.LogInformation("Worker executed at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }


}
