using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFarmingGame.Domains.Response
{
    public class UserResponse
    {
        public string Alias { get; set; }
        public int Money { get; set; }
        public int StealAmount { get; set; }
        public int ProtectAmount { get; set; }
        public string? token { get; set; }
        public List<Land>? Lands { get; set; }
    }
}
