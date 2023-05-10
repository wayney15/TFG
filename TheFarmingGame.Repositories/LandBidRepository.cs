using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        public async Task CreateLandBidAsync(LandBid landBid)
        {
            try
            {
                await _theFarmingGameDbContext.LandBids.AddAsync(landBid);
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

        public async Task<LandBid?> GetLandBidByLandIdAsync(int landId)
        {
            return await _theFarmingGameDbContext.LandBids.Where(l => l.LandId == landId).FirstOrDefaultAsync();
        }

    }
}
