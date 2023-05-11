using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFarmingGame.Domains;

namespace TheFarmingGame.Repositories
{
    public interface IBidRepository
    {
        Task AddBidAsync(Bid bid);
        Task<IEnumerable<Bid>> GetBidsByLandBidIdAsync(int landBidId);
        Task<IEnumerable<Bid>> GetBidsByLandBidIdAndUserIdAsync(int landBidId, int userId);
        Task<IEnumerable<Bid>> GetAllBidsAsync();
    }
}
