using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFarmingGame.Domains;

namespace TheFarmingGame.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        // add hash and other helper functions here
        public Task<String> Register()
        {
            // call service authorization functions
            return null;
        }
        public Task<User> Login()
        {
            return null;
        }
    }
}
