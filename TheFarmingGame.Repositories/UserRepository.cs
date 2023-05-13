using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFarmingGame.Domains;

namespace TheFarmingGame.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TheFarmingGameDbContext _theFarmingGameDbContext;

        public UserRepository(TheFarmingGameDbContext theFarmingGameDbContext)
        {
            _theFarmingGameDbContext = theFarmingGameDbContext;
        }

        public async Task CreateUserAsync(User user)
        {
            try {
                await _theFarmingGameDbContext.Users.AddAsync(user);
                await _theFarmingGameDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exceptions
                throw new Exception("Error saving entity.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("Error saving entity.", ex);
            }
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _theFarmingGameDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetUserByAliasAsync(string alias)
        {
            return await _theFarmingGameDbContext.Users.FirstOrDefaultAsync(u => u.Alias == alias);
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _theFarmingGameDbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<IEnumerable<User>> GetAllUsersExceptSelfAsync(int selfId)
        {
            return await _theFarmingGameDbContext.Users.Where(u => u.Id != selfId).ToListAsync();
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _theFarmingGameDbContext.Users.ToListAsync();
        }
        public async Task<User?> UpdateLand(User user)
        {
            var result = await _theFarmingGameDbContext.Users
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            if (result != null)
            {
                result.Lands = user.Lands;
                result.Money = user.Money;
                result.ProtectAmount = user.ProtectAmount;
                result.StealAmount = user.StealAmount;

                await _theFarmingGameDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
