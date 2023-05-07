using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFarmingGame.Services
{
    public interface IBidService
    {
        Task<String> ListBids();
        Task<String> ListUserBids(string Id);
    }
}
