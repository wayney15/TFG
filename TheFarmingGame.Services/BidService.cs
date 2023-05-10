using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFarmingGame.Domains;
using TheFarmingGame.Repositories;

namespace TheFarmingGame.Services
{
    public class BidService : IBidService
    {
        private readonly IBidRepository _bidRepository;
        public BidService(IBidRepository bidRepository)
        {
            _bidRepository = bidRepository;
        }
        public async Task AddBidAsync(Bid bid)
        {
            try
            {
                await _bidRepository.AddBidAsync(bid);
            }
            catch (Exception ex)
            {
                // Log the error and return an error result to the caller
                throw new Exception("Error saving entity.", ex);
            }
        }

        public async Task<IEnumerable<Bid>> GetBidsByLandBidIdAsync(int landBidId)
        {
            return await _bidRepository.GetBidsByLandBidIdAsync(landBidId); 
        }
        public async Task<IEnumerable<Bid>> GetBidsByLandBidIdAndUserIdAsync(int landBidId, int userId)
        {
            return await _bidRepository.GetBidsByLandBidIdAndUserIdAsync(landBidId, userId);
        }
    }
}
