using Microsoft.Extensions.Configuration;
using TheFarmingGame.Domains;
using TheFarmingGame.Services;
using TheFarmingGame.Repositories;

public class LandGeneratorService : IHostedService
{
    private readonly ILogger<LandGeneratorService> _logger;
    private readonly ILandService _landService;

    public LandGeneratorService(ILogger<LandGeneratorService> logger,  IServiceProvider _serviceProvider)
    {
        _logger = logger;
        _landService = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ILandService>();

    }

    public async Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service running.");

        while(!stoppingToken.IsCancellationRequested)
        {
            for(int i = 0; i < 3; i++)
            {
                // starterLand.Id = starterLand.Id + 1;
                // starterLand.Alias = "Land" + starterLand.Id;
                try
                {
                        await _landService.GenerateNewLand();
                        _logger.LogInformation("New Land Made");
                }
                catch (Exception ex)
                {
                    // Log the error and return an error result to the caller
                    throw new Exception("Error creating new land.", ex);
                }
            }
            _logger.LogInformation("3 New Lands Made");
            Task delayTask = Task.Delay(TimeSpan.FromMinutes(5));
            delayTask.Wait();
        }

        return;
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Stopped producing Land.");

        return Task.CompletedTask;
    }
}