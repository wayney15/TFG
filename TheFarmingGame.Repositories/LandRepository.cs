using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFarmingGame.Domains;

namespace TheFarmingGame.Repositories
{
    public class LandRepository : RepositoryBase<Land>, ILandRepository
    {
        public LandRepository(TheFarmingGameDbContext theFarmingGameDbContext) : base(theFarmingGameDbContext)
        {
        }
    }
}
