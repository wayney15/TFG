using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFarmingGame.Domains.Response
{
    public class UserResponse
    {
        public string? Alias { get; set; }
        public int Money { get; set; }
        public int ProtectAmount { get; set; }
        public UserResponse(User user)
        {
            Alias = user.Alias;
            Money = user.Money;
            ProtectAmount = user.ProtectAmount;
        }
    }
}
