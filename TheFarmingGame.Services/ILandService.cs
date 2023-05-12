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
        Task GenerateNewLand();
        Task<IEnumerable<Land>?> GetAllLandAsync();
        Task<String> ListLands();
        Task<String> ListUserLands(string Id);
    }
}
