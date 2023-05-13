using Microsoft.Extensions.Configuration;
using TheFarmingGame.Domains;
using TheFarmingGame.Services;
using TheFarmingGame.Repositories;

public class LandGeneratorService : BackgroundService
{
    private readonly ILogger<LandGeneratorService> _logger;
    private readonly ILandService _landService;
    private readonly ILandBidService _landBidService;

    public LandGeneratorService(ILogger<LandGeneratorService> logger,  IServiceProvider _serviceProvider)
    {
        _logger = logger;
        _landService = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ILandService>();
        _landBidService = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ILandBidService>();

    }

    public async Task StartAsync(CancellationToken stoppingToken)
    {
        return;
    }

    public async Task StopAsync(CancellationToken stoppingToken)
    {
        return;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested)
        {
            for(int i = 0; i < 3; i++)
            {
                try
                {
                    var newLand = await _landService.GenerateNewLand();
                    _logger.LogInformation("New Land Made");
                    var newLandBid = new LandBid()
                    {
                        LandId = newLand.Id,
                        ExpirationTime = DateTime.Now.AddMinutes(5)
                    };
                    await _landBidService.GenerateNewLandBid(newLandBid);
                    _logger.LogInformation("New Land Bid Made");
                }
                catch (Exception ex)
                {
                    // Log the error and return an error result to the caller
                    throw new Exception("Error creating new land.", ex);
                }
            }
            _logger.LogInformation("3 New Lands Made");
            Task.Delay(TimeSpan.FromMinutes(3)).Wait();
        }
        return;
    }
}