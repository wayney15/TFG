using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFarmingGame.Domains.Requests
{
    public class LeaderboardResponse
    {
        public string? Alias { get; set; }
        public int Money { get; set; }
    }
}
