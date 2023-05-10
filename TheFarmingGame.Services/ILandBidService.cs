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
        Task<LandBid?> GetLandBidByLandIdAsync(int landId);
    }
}
