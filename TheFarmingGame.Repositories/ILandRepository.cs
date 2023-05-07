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
        Task<Land> CreateLandAsync(Land land);
        Task<Land?> GetLandByIdAsync(int id);
        Task<Land?> UpdateLand(Land land);
    }
}
