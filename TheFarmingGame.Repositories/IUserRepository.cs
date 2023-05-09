using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFarmingGame.Domains;

namespace TheFarmingGame.Repositories
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User user);
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByAliasAsync(string alias);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<IEnumerable<User>?> GetAllUsersExceptSelfAsync(int selfId);
        Task<User?> UpdateLand(User user);

    }
}
