using DotNetCore.CAP;
using EmailWindowsService.Services;
using EmailWindowsService.Services.Interfaces;

namespace EmailWindowsService
{
    public class Worker : BackgroundService, ICapSubscribe
    {
        private readonly IEmailSender _emailSender;

    
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger,IEmailSender emailSender)
        {
            _logger = logger;
            _emailSender = emailSender;

        }
      
     
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
