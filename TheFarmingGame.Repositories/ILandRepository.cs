using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFarmingGame.Domains;

namespace TheFarmingGame.Repositories
{
    public interface ILandRepository 
    {
        Task CreateLandAsync(Land land);
        Task<Land?> GetLandByIdAsync(int id);
        Task<IEnumerable<Land>> GetAllLandAsync();
        Task<Land?> UpdateLand(Land land);
    }
}
