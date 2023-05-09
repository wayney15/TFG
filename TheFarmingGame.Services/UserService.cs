using Konscious.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TheFarmingGame.Domains;
using TheFarmingGame.Repositories;
using TheFarmingGame.Domains;
using TheFarmingGame.Domains.Response;

namespace TheFarmingGame.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public UserService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;

        }

        private static string GetSalt()
        {
            var bytes = new byte[32];
            /*https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.getbytes?view=net-7.0 provides cryptographically strong random number
             */
            var rng = RandomNumberGenerator.Create();
            rng.GetNonZeroBytes(bytes);
            return Encoding.UTF8.GetString(bytes);
        }

        private static string Hash(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var passwordByte = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(passwordByte);
                return Convert.ToBase64String(hash);
            }

        }

        public async Task Register(string username, string password, string alias)
        {
            // call service authorization functions
            var salt = GetSalt();
            var saltedPassword = password + salt;
            var hashedPassword = Hash(saltedPassword);
            var newUser = new User
            {
                UserName = username,
                Alias = alias,
                Password = hashedPassword,
                Money = 0,
                Lands = null,
                ProtectAmount = 0,
                Salt = salt,
                StealAmount = 0,
            };
            try
            {
                await _userRepository.CreateUserAsync(newUser);
            }
            catch (Exception ex)
            {
                // Log the error and return an error result to the caller
                throw new Exception("Error saving entity.", ex);
            }
        }

        public async Task<User?> Login(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null)
            {
                return null;
            }

            var saltedPassword = password + user.Salt;
            var hashedPassword = Hash(saltedPassword);
            if (hashedPassword.Equals(user.Password))
            {
                return user;
            }
            else
                return null;
        }
        public Task<UserResponse> Get(string Id)
        {
            //User user = _userRepository.GetUserByIdAsync(Convert.ToInt32(Id));


    }
}
