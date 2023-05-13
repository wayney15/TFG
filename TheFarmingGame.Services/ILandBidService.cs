using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFarmingGame.Domains;

namespace TheFarmingGame.Services
{
    public interface ILandBidService
    {
        Task GenerateNewLandBid(LandBid landBid);
        Task<LandBid?> GetLandBidByLandIdAsync(int landId);
        Task<IEnumerable<LandBid>> GetAllLandBidsByIdsAsync(List<int> ids);
        Task<IEnumerable<LandBid>> GetAllLandBidsAsync();
        Task<IEnumerable<LandBid>> GetAllActiveLandBidsAsync();
        Task<IEnumerable<LandBid>> GetAllInActiveLandBidsAsync();
    }
}
