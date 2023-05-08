using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFarmingGame.Repositories;
using TheFarmingGame.Domains;

namespace TheFarmingGame.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;
        public Task<String> UserInfo(string Id)
        {
            User user = _userRepository.GetUserByIdAsync(Convert.ToInt32(Id));
            return ("{ Alias: " + user.Alias + ", Money: " + user.Money + ", StealAmount: " + user.StealAmount + ", ProtectAmount: " + user.ProtectAmount + "}");
        }
        public Task<String> ListUsers()
        {
            return null;
        }
    }
}
