using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFarmingGame.Repositories;
using TheFarmingGame.Domains;
using TheFarmingGame.Domains.Response;

namespace TheFarmingGame.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<UserResponse> Get(string Id)
        {
            //User user = _userRepository.GetUserByIdAsync(Convert.ToInt32(Id));

            return null;
        }
        public Task<List<UserResponse>> ListUsers()
        {
            return null;
        }
    }
}
