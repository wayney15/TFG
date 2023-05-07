using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFarmingGame.Services
{
    public interface IUserService
    {
        
        Task<String> UserInfo(string Id);
        Task<String> ListUsers();
    }
}
