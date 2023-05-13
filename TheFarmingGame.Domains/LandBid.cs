using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFarmingGame.Domains
{
    public class LandBid
    {  
        public int Id { get; set; }
        public int LandId { get; set; }
        public DateTime ExpirationTime { get; set; }
        public bool Is_finished { get; set; }
    }
}
