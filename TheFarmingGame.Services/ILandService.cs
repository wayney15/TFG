using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFarmingGame.Domains;
using TheFarmingGame.Repositories;

namespace TheFarmingGame.Services
{
    public interface ILandService
    {
        Task<Land> GenerateNewLand();
        Task<IEnumerable<Land>> GetAllLandAsync();
        Task<Land?> GetLandByIdAsync(int Id);
        Task<IEnumerable<Land>> GetLandByUserIdAsync(int UserId);
        Task<Land?> UpdateLand(Land land);
    }
}
