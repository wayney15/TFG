﻿﻿using Konscious.Security.Cryptography;
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
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password)); 
            argon2.DegreeOfParallelism = 24;
            argon2.KnownSecret = Encoding.UTF8.GetBytes("ISA681");
            // argon2.AssociatedData = Should be userID
            argon2.Iterations = 24;
            argon2.MemorySize = 4096;
            return Convert.ToBase64String(argon2.GetBytes(256));
        }

        public async Task RegisterAsync(string username, string password, string alias)
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
                Money = 2000,
                Lands = null,
                ProtectAmount = 0,
                Salt = salt,
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

        public async Task<User?> LoginAsync(string username, string password)
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

        public async Task<IEnumerable<User>> GetAllUsersExceptSelfAsync(int selfId)
        {
            return await _userRepository.GetAllUsersExceptSelfAsync(selfId);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<User?> UpdateUser(User user)
        {
            return await _userRepository.UpdateUser(user);
        }
    }
}