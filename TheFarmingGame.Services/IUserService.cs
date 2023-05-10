using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFarmingGame.Domains;

namespace TheFarmingGame.Services
{
    public interface IUserService
    {
        Task RegisterAsync(string username, string password, string alias);
        Task<User?> LoginAsync(string username, string password);
        Task<IEnumerable<User>> GetAllUsersExceptSelfAsync(int selfId);
        Task<User?> GetUserByIdAsync(int id);
    }
}
