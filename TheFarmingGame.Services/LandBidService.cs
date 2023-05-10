using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFarmingGame.Domains;
using TheFarmingGame.Repositories;

namespace TheFarmingGame.Services
{
    public class LandBidService : ILandBidService
    {
        private readonly LandBidRepository _landBidRepository;
        public LandBidService(LandBidRepository landBidRepository)
        {
            _landBidRepository = landBidRepository;
        }

        public async Task<LandBid?> GetLandBidByLandIdAsync(int landId)
        {
            return await _landBidRepository.GetLandBidByLandIdAsync(landId);
        }
    }
}
