using Microsoft.Extensions.Configuration;
using TheFarmingGame.Domains;
using TheFarmingGame.Repositories;

public class LandGeneratorService : IHostedService
{
    private readonly ILandRepository _landRepository;
    private readonly IConfiguration _configuration;
    public LandGeneratorService(IConfiguration configuration, ILandRepository landRepository)
    {
        _configuration = configuration;
        _landRepository = landRepository;

    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Land starterLand = new Land();
        starterLand.Id = 0;
        starterLand.HarvestTime = new DateTime();
        starterLand.BidTime = new DateTime();
        starterLand.Level = 1;
        starterLand.Plant = 0;
        starterLand.HarvestTime = DateTime.Now;
        starterLand.IsProtected = false;
        starterLand.UserId = 0;
        starterLand.UserAlias = "N/A";

        while(!cancellationToken.IsCancellationRequested)
        {
            for(int i = 0; i < 3; i++)
            {
                starterLand.Id = starterLand.Id + 1;
                starterLand.Alias = "Land" + starterLand.Id;
                try
                {
                    await _landRepository.CreateLandAsync(starterLand);
                }
                catch (Exception ex)
                {
                    // Log the error and return an error result to the caller
                    throw new Exception("Error creating new land.", ex);
                }
            }
            Console.Write("3 New Lands made");
            Task dummyTask = Task.Delay(TimeSpan.FromMinutes(5));
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return null;
    }
}