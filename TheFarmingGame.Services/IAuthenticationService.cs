using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFarmingGame.Domains;
using TheFarmingGame.Domains.Response;

namespace TheFarmingGame.Services
{
    public interface IAuthenticationService
    {
        Task<UserResponse> Register(string UserName, string Password, string Alias);
        Task<UserResponse> Login(string UserName, string Password);
    }
}
