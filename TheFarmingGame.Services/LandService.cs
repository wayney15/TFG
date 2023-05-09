using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFarmingGame.Domains;
using TheFarmingGame.Repositories;

namespace TheFarmingGame.Services
{
    public class LandService : ILandService
    {
        private readonly ILandRepository _landRepository;

        public LandService(ILandRepository landRepository)
        {
            _landRepository = landRepository;
        }

        public async Task<Land> GenerateNewLand()
        {
            var newLand = new Land();
            var result = await _landRepository.CreateLandAsync(newLand);
            return result;
        }

        public Task<String> ListLands()
        {
            return null;
        }
        public Task<String> ListUserLands(string Id)
        {
            return null;
        }

        public async Task<IEnumerable<Land>?> GetAllLandAsync()
        {
            return await _landRepository.GetAllLandAsync();
        }
    }
}
