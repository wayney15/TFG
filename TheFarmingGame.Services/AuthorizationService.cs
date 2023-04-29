using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using TheFarmingGame.Domains;

//Reference: https://github.com/kmaragon/Konscious.Security.Cryptography

namespace TheFarmingGame.Services
{
    public class AuthorizationService : IAuthorizationService
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
            var argon2 = new Konscious.Security.Cryptography.Argon2i(Encoding.UTF8.GetBytes(password));
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

        public String Register()
        {
            // call service authorization functions
            return null;
        }
        public User Login()
        {
            return null;
        }
    }
}
