using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFarmingGame.Domains;

namespace TheFarmingGame.Repositories
{
    public interface ILandBidRepository
    {
        Task CreateLandBidAsync(LandBid landBid);
        Task<LandBid?> GetLandBidByLandIdAsync(int landId);
        Task<IEnumerable<LandBid>> GetAllLandBidsByIdsAsync(List<int> ids);
        Task<IEnumerable<LandBid>> GetAllLandBidsAsync();
        Task<IEnumerable<LandBid>> GetAllActiveLandBidsAsync();
        Task<IEnumerable<LandBid>> GetAllInActiveLandBidsAsync();
    }
}
