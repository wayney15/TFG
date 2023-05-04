using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFarmingGame.Domains;

namespace TheFarmingGame.Services
{
    public interface IAuthorizationService
    {
        Task<String> Register();
        Task<User> Login();
    }
}
