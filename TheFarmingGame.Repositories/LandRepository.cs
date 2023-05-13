using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFarmingGame.Domains;

namespace TheFarmingGame.Repositories
{
    public class LandRepository : ILandRepository
    {
        private readonly TheFarmingGameDbContext _theFarmingGameDbContext;

        public LandRepository(TheFarmingGameDbContext theFarmingGameDbContext)
        {
            _theFarmingGameDbContext = theFarmingGameDbContext;
        }
        public async Task<Land> CreateLandAsync(Land land)
        {
            try
            {
                await _theFarmingGameDbContext.Lands.AddAsync(land);
                await _theFarmingGameDbContext.SaveChangesAsync();
                return land;
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

        public async Task<Land?> GetLandByIdAsync(int id)
        {
            return await _theFarmingGameDbContext.Lands.FirstOrDefaultAsync(l => l.Id == id);
        }
        public async Task<IEnumerable<Land>> GetLandByUserIdAsync(int UserId)
        {
            return await _theFarmingGameDbContext.Lands.Where(l => l.UserId == UserId).ToListAsync();
        }

        public async Task<IEnumerable<Land>> GetAllLandAsync()
        {
            return await _theFarmingGameDbContext.Lands.ToListAsync();
        }

        public async Task<Land?> UpdateLand(Land land)
        {
            var result = await _theFarmingGameDbContext.Lands
                .FirstOrDefaultAsync(l => l.Id == land.Id);

            if (result != null)
            {
                result.Alias = land.Alias;
                result.Level = land.Level;
                result.Plant = land.Plant;
                result.HarvestTime = land.HarvestTime;
                result.IsProtected = land.IsProtected;
                result.UserId = land.Id;

                await _theFarmingGameDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
