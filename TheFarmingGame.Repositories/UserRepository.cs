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

        public async Task<User> CreateUserAsync(User user)
        {
            var result = await _theFarmingGameDbContext.Users.AddAsync(user);
            await _theFarmingGameDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _theFarmingGameDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetUserByAliasAsync(int id)
        {
            return await _theFarmingGameDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
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
