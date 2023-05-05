using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFarmingGame.Domains
{
    public class Bid
    {
        public int Id { get; set; }
        public int LandId { get; set; }
        public int UserId { get; set; }
        public int BidAmount { get; set; }
    }
}
