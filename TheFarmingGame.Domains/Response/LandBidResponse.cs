using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFarmingGame.Domains.Response
{
    public class LandBidResponse
    {
        public int LandId { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
