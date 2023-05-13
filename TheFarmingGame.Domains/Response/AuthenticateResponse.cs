using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFarmingGame.Domains.Response
{
    public class AuthenticateResponse
    {
        public string? Alias { get; set; }
        public int Money { get; set; }
        public int ProtectAmount { get; set; }
        public string? Token { get; set; }

        public AuthenticateResponse(User user, string token)
        {
            Alias = user.Alias;
            Money = user.Money;
            Token = token;
            ProtectAmount = user.ProtectAmount;
        }
    }
}
