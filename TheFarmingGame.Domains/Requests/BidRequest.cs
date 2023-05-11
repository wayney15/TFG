using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFarmingGame.Domains.Requests
{
    public class BidRequest
    {
        public int LandId { get; set; }
        public int Amount { get; set; }
    }
}
