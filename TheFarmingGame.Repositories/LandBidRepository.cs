using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFarmingGame.Domains;

namespace TheFarmingGame.Repositories
{
    public class LandBidRepository : ILandBidRepository
    {
        private readonly TheFarmingGameDbContext _theFarmingGameDbContext;
        public LandBidRepository(TheFarmingGameDbContext theFarmingGameDbContext)
        {
            _theFarmingGameDbContext = theFarmingGameDbContext;
        }
        public async Task<LandBid> CreateLandBidAsync(LandBid landBid)
        {
            var result = await _theFarmingGameDbContext.LandBids.AddAsync(landBid);
            await _theFarmingGameDbContext.SaveChangesAsync();
            return result.Entity;
        }

    }
}
