using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Konscious.Security.Cryptography;
using System.Threading.Tasks;
using TheFarmingGame.Domains;
using TheFarmingGame.Domains.Response;

namespace TheFarmingGame.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public static string Salt()
        {
            var bytes = new byte[128];
            var rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(bytes);
            return Encoding.UTF8.GetString(bytes);
        }

        public static byte[] Hash(string password, string salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));
            argon2.DegreeOfParallelism = 24;
            argon2.Salt = Encoding.UTF8.GetBytes(salt);
            argon2.KnownSecret = Encoding.UTF8.GetBytes("ISA681");
            // argon2.AssociatedData = Should be userID
            argon2.Iterations = 24;
            argon2.MemorySize = 4096;
            return argon2.GetBytes(256);
        }

        public static void Main()
        {
            var SampleHash = Hash("SamplePassword", Salt());
            Console.WriteLine("Sample hash: {0}", SampleHash);
        }
        // add hash and other helper functions here
        public Task<UserResponse> Register(string UserName, string Password, string Alias)
        {
            // call service authorization functions
            return null;
        }
        public Task<UserResponse> Login(string UserName, string Password)
        {
            return null;
        }
    }
}
