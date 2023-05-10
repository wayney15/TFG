using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFarmingGame.Domains;

namespace TheFarmingGame.Repositories
{
    public class BidRepository : IBidRepository
    {
        private readonly TheFarmingGameDbContext _theFarmingGameDbContext;

        public BidRepository(TheFarmingGameDbContext theFarmingGameDbContext)
        {
            _theFarmingGameDbContext = theFarmingGameDbContext;
        }

        public async Task AddBidAsync(Bid bid)
        {
            try
            {
                await _theFarmingGameDbContext.Bids.AddAsync(bid);
                await _theFarmingGameDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exceptions
                throw new Exception("Error saving entity.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("Error saving entity.", ex);
            }
        }

        public async Task<IEnumerable<Bid>> GetBidsByLandBidIdAsync(int landBidId)
        {
            return await _theFarmingGameDbContext.Bids.Where(b => b.LandBidId == landBidId).ToListAsync();
        }

        public async Task<IEnumerable<Bid>> GetBidsByLandBidIdAndUserIdAsync(int landBidId, int userId)
        {
            return await _theFarmingGameDbContext.Bids.Where(b => b.LandBidId == landBidId && b.UserId == userId).ToListAsync();
        }
    }
}
