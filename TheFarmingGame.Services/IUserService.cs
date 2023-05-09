using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFarmingGame.Domains.Response;

namespace TheFarmingGame.Services
{
    public interface IUserService
    {
        Task<UserResponse> Get(string Id);
        Task<List<UserResponse>> ListUsers();
    }
}
