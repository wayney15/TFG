using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFarmingGame.Domains;

namespace TheFarmingGame.Services
{
    public interface IGameplayService
    {
        Task<String> UserInfo(string Id);
        Task<String> ListUsers();
        Task<String> ListLands();
        Task<String> ListBids();
        Task<String> ListUserBids(string Id);
        Task<String> ListUserLands(string Id);

    }
}
