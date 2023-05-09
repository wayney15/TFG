using Microsoft.Extensions.Configuration;
using TheFarmingGame.Domains;
using TheFarmingGame.Services;

public class LandGeneratorService : BackgroundService
{
    private readonly ILandService _landService;
    private readonly IConfiguration _configuration;
    public IServiceProvider Services { get; }
    public LandGeneratorService(IConfiguration configuration, ILandService landService)
    {
        _configuration = configuration;
        _landService = landService;

    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
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
                    using (var scope = Services.CreateScope())
                    {
                        var scopedProcessingService = 
                        scope.ServiceProvider
                        .GetRequiredService<LandService>();

                    await _landService.GenerateNewLand(starterLand);
                    }
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
}