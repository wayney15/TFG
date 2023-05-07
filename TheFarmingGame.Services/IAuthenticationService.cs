using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFarmingGame.Domains;

namespace TheFarmingGame.Services
{
    public interface IAuthenticationService
    {
        Task<String> Register(string UserName, string Password, string Alias);
        Task<User> Login(string UserName, string Password);
    }
}
