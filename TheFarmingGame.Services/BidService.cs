using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFarmingGame.Repositories;

namespace TheFarmingGame.Services
{
    public class BidService : IBidService
    {
        private readonly IBidRepository _bidRepository;
        public BidService(IBidRepository bidRepository)
        {
            _bidRepository = bidRepository;
        }
        public Task<String> ListBids()
        {
            return null;
        }
        public Task<String> ListUserBids(string Id)
        {
            return null;
        }
    }
}
