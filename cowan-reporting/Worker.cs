using OperationsReporting.Repositories;

namespace OperationsReporting
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //var dataRepo = new CowanDataRepository();
        }
    }
}
