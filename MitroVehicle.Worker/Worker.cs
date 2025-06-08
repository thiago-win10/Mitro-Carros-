using MediatR;
using MitroVehicle.Application.StatusLocationUpdateService;
using System.Diagnostics;

namespace MitroVehicle.UpdateStatusLocation
{
    public class Worker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<Worker> _logger;
        private readonly IHostApplicationLifetime _applicationLifetime;
        private CancellationToken _cancellationToken;

        public Worker(IServiceProvider serviceProvider, ILogger<Worker> logger, IHostApplicationLifetime applicationLifetime)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _applicationLifetime = applicationLifetime;  
            
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _cancellationToken = stoppingToken;

            _logger.LogInformation("Worker init.");

            var sw = new Stopwatch();

            try
            {
                sw.Start();
                await RunAsync(_cancellationToken);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            finally
            {
                _applicationLifetime.StopApplication();
                Dispose();
                sw.Stop();
                _logger.LogInformation("Worker is finished {s} seconds", sw.Elapsed.TotalSeconds);
            }
          
        }

        private async Task RunAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                await mediator.Send(new StatusLocationUpdateRequest(), stoppingToken);
            }
        }
    }
}
