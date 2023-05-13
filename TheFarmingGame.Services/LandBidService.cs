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
        private readonly ILandBidRepository _landBidRepository;
        public LandBidService(ILandBidRepository landBidRepository)
        {
            _landBidRepository = landBidRepository;
        }

        public async Task GenerateNewLandBid(LandBid landBid)
        {
            try
            {
                await _landBidRepository.CreateLandBidAsync(landBid);
            }
            catch (Exception ex)
            {
                // Log the error and return an error result to the caller
                throw new Exception("Error saving entity.", ex);
            }
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
