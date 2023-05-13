using Microsoft.Extensions.Configuration;
using TheFarmingGame.Domains;
using TheFarmingGame.Services;
using TheFarmingGame.Repositories;
using System.Collections.Generic;

public class LandGeneratorService : BackgroundService
{
    private readonly ILogger<LandGeneratorService> _logger;
    private readonly ILandService _landService;
    private readonly ILandBidService _landBidService;
    private readonly IBidService _bidService;
    private readonly IUserService _userService;

    public LandGeneratorService(ILogger<LandGeneratorService> logger,  IServiceProvider _serviceProvider)
    {
        _logger = logger;
        _landService = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ILandService>();
        _landBidService = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ILandBidService>();
        _bidService = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IBidService>();
        _userService = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IUserService>();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        const int interval = 3;
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
                        ExpirationTime = DateTime.Now.AddMinutes(interval),
                        Is_finished = false
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
            var cur_landbids = await _landBidService.GetAllUnParsedLandBidsAsync();
            _logger.LogInformation("cur_landbids count: " + cur_landbids.Count().ToString());
            if(cur_landbids.Any())
            {
                foreach (LandBid landBid in cur_landbids)
                {
                    if (landBid.ExpirationTime <= DateTime.Now)
                    {
                        var cur_bids = (await _bidService.GetBidsByLandBidIdAsync(landBid.Id)).ToList();
                        while (cur_bids.Any())
                        {
                            var max_bid = cur_bids.OrderByDescending(b => b.BidAmount).First();
                            var land_on_bid = await _landService.GetLandByIdAsync(landBid.LandId);
                            if (land_on_bid == null)
                            {
                                _logger.LogError("Landid was found in landbid but not in land table: " + landBid.LandId.ToString());
                                break;
                            }
                            land_on_bid.UserId = max_bid.UserId;
                            var user = await _userService.GetUserByIdAsync(max_bid.UserId);

                            // if user doesnt have enough money, invalidate this bid
                            if (user == null || user.Money < max_bid.BidAmount) {
                                _logger.LogError("Bid was removed because user did not have enough money.");
                                // TODO: add this to a history
                                cur_bids.Remove(max_bid);
                                continue;
                            }
                            user.Money -= max_bid.BidAmount;
                            await _userService.UpdateUser(user);
                            await _landService.UpdateLand(land_on_bid);
                            landBid.Is_finished = true;
                            await _landBidService.UpdateLandBid(landBid);
                        }

                    }
                        
                    
                }

            }
            Task.Delay(TimeSpan.FromMinutes(interval)).Wait();
        }
        return;
    }
}