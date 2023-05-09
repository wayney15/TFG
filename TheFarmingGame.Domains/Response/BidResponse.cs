using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFarmingGame.Domains.Response
{
    public class BidResponse
    {
        public string LandAlias { get; set; }
        public string UserAlias { get; set; }
        public int BidAmount { get; set; }
    }
}
