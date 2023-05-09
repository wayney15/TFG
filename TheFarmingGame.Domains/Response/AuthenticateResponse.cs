using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFarmingGame.Domains.Response
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            Alias = user.Alias;
            Token = token;
        }
    }
}
