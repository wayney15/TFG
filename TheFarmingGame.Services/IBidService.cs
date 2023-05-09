using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFarmingGame.Domains;

namespace TheFarmingGame.Services
{
    public interface IBidService
    {
        Task<IEnumerable<Bid>?> GetAllBidAsync();
        Task<String> ListBids();
        Task<String> ListUserBids(string Id);
    }
}
