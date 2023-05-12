using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFarmingGame.Domains.Response
{
    public class LandResponse
    {
        public int Id { get; set; }
        public string? Alias { get; set; }
        public int Level { get; set; }
        public int Plant { get; set; }
        public DateTime? HarvestTime { get; set; }
        public bool IsProtected { get; set; }
        public string? UserAlias { get; set; }
    }
}
