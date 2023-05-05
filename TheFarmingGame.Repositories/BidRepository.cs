using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFarmingGame.Domains;

namespace TheFarmingGame.Repositories
{
    public class BidRepository : RepositoryBase<Bid>, IBidRepository
    {
        public BidRepository(TheFarmingGameDbContext theFarmingGameDbContext) : base(theFarmingGameDbContext)
        {
        }
    }
}
