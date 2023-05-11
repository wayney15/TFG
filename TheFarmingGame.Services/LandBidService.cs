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
        public async Task<IEnumerable<LandBid>> GetAllLandBidsByIdsAsync(List<int> ids)
        {
            return await _landBidRepository.GetAllLandBidsByIdsAsync(ids);
        }
        public async Task<IEnumerable<LandBid>> GetAllLandBidsAsync()
        {
            return await _landBidRepository.GetAllLandBidsAsync();
        }
        public async Task<IEnumerable<LandBid>> GetAllActiveLandBidsAsync()
        {
            return await _landBidRepository.GetAllActiveLandBidsAsync();
        }
        public async Task<IEnumerable<LandBid>> GetAllInActiveLandBidsAsync()
        {
            return await _landBidRepository.GetAllInActiveLandBidsAsync();
        }
    }
}
