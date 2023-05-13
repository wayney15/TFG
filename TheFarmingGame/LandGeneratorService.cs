using Microsoft.Extensions.Configuration;
using TheFarmingGame.Domains;
using TheFarmingGame.Services;
using TheFarmingGame.Repositories;

public class LandGeneratorService : BackgroundService
{
    private readonly ILogger<LandGeneratorService> _logger;
    private readonly ILandService _landService;
    private readonly ILandBidService _landBidService;
    private readonly IBidService _bidService;

    public LandGeneratorService(ILogger<LandGeneratorService> logger,  IServiceProvider _serviceProvider)
    {
        _logger = logger;
        _landService = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ILandService>();
        _landBidService = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ILandBidService>();
        _bidService = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IBidService>();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested)
        {
            // generate new lands
            for(int i = 0; i < 3; i++)
            {
                try
                {
                    var newLand = await _landService.GenerateNewLand();
                    _logger.LogInformation("New Land Made");
                    var newLandBid = new LandBid()
                    {
                        LandId = newLand.Id,
                        ExpirationTime = DateTime.Now.AddMinutes(3)
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

            //check the old bid and process those
            var cur_landbids = await _landBidService.GetAllActiveLandBidsAsync();
            if(!cur_landbids.Any())
            {
                foreach (LandBid landBid in cur_landbids)
                {
                    if (landBid.ExpirationTime >= DateTime.Now)
                    {
                        var cur_bids = await _bidService.GetBidsByLandBidIdAsync(landBid.Id);
                        if (cur_bids.Any())
                        {
                            var max_bid = cur_bids.OrderByDescending(b => b.BidAmount).First();
                            var land_on_bid = await _landService.GetLandByIdAsync(landBid.LandId);
                            if (land_on_bid == null)
                            {
                                _logger.LogError("Landid was found in landbid but not in land table: " + landBid.LandId.ToString());
                                break;
                            }
                            land_on_bid.UserId = max_bid.UserId;
                            await _landService.UpdateLand(land_on_bid);

                        }

                    }
                        
                    
                }

            }
            Task.Delay(TimeSpan.FromMinutes(3)).Wait();
        }
        return;
    }
}