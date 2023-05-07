using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Konscious.Security.Cryptography;
using System.Threading.Tasks;
using TheFarmingGame.Domains;

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
        public Task<User> Register(string UserName, string Password, string Alias)
        {
            int SChars, UChars, LChars, Nums, Space;

            (SChars, UChars, LChars, Nums, Space) = InputValidation(UserName);
            if(SChars != 0){
                return null; //"Invalid input, special characters are not allowed."
            }
            else if(UChars < 1){
                return null; //"Invalid input, at least 1 uppercase character required."
            }
            else if(LChars < 1){
                return null; //"Invalid input, at least 1 lowercase character required."
            }
            else if(Nums < 2){
                return null; //"Invalid input, at least 2 numbers required."
            }
            else if(Space != 0){
                return null; //"Invalid input, spaces are not allowed."
            }
            else if(UserName.Length > 30 || UserName.Length < 8){
                return null; //"Invalid input, usernames must be 8 to 30 characters."
            }
            else{
                Console.Write("Input Username valid.");
            }
            
            (SChars, UChars, LChars, Nums, Space) = InputValidation(Password);
            if(SChars < 2){
                return null; //"Invalid input, at least 2 special characters required."
            }
            else if(UChars < 1){
                return null; //"Invalid input, at least 1 uppercase character required."
            }
            else if(LChars < 1){
                return null; //"Invalid input, at least 1 lowercase character required."
            }
            else if(Nums < 2){
                return null; //"Invalid input, at least 2 numbers required."
            }
            else if(Space != 0){
                return null; //"Invalid input, spaces are not allowed."
            }
            else if(Password.Length > 30 || Password.Length < 8){
                return null; //"Invalid input, usernames must be 8 to 30 characters."
            }
            else{
                Console.Write("Input Password valid.");
            }

            (SChars, UChars, LChars, Nums, Space) = InputValidation(Alias);
            if(SChars != 0){
                return null; //"Invalid input, special characters are not allowed."
            }
            else if(LChars < 1){
                return null; //"Invalid input, at least 1 lowercase character required."
            }
            else{
                Console.Write("Input Alias valid.");
            }

            //Need to execute a command here to see if username already exists
            //Should we allow multiple users to have the same Alias?

            return null;
        }
        public Task<User> Login(string UserName, string Password)
        {
            int SChars, UChars, LChars, Nums, Space;            

            (SChars, UChars, LChars, Nums, Space) = InputValidation(UserName);
            if(SChars != 0){
                return null; //"Invalid input, special characters are not allowed."
            }
            else if(UChars < 1){
                return null; //"Invalid input, at least 1 uppercase character required."
            }
            else if(LChars < 1){
                return null; //"Invalid input, at least 1 lowercase character required."
            }
            else if(Nums < 2){
                return null; //"Invalid input, at least 2 numbers required."
            }
            else if(Space != 0){
                return null; //"Invalid input, spaces are not allowed."
            }
            else if(UserName.Length > 30 || UserName.Length < 8){
                return null; //"Invalid input, usernames must be 8 to 30 characters."
            }
            else{
                Console.Write("Input Username valid.");
            }
            
            (SChars, UChars, LChars, Nums, Space) = InputValidation(Password);
            if(SChars < 2){
                return null; //"Invalid input, at least 2 special characters required."
            }
            else if(UChars < 1){
                return null; //"Invalid input, at least 1 uppercase character required."
            }
            else if(LChars < 1){
                return null; //"Invalid input, at least 1 lowercase character required."
            }
            else if(Nums < 2){
                return null; //"Invalid input, at least 2 numbers required."
            }
            else if(Space != 0){
                return null; //"Invalid input, spaces are not allowed."
            }
            else if(Password.Length > 30 || Password.Length < 8){
                return null; //"Invalid input, usernames must be 8 to 30 characters."
            }
            else{
                Console.Write("Input Password valid.");
            }

            return null;
        }

        public (int, int, int, int, int) InputValidation(string str)
        {
            int SChars = 0;
            int UChars = 0;
            int LChars = 0;
            int Nums = 0;
            int Space = 0;
            char[] arr = str.ToCharArray();
            for (int i = 0; i < arr.Length; i++) {
            if (!Char.IsLetterOrDigit(arr[i])) {
               SChars = SChars + 1;
            }
            if (Char.IsUpper(arr[i])) {
               UChars = UChars + 1;
            }
            if (Char.IsLower(arr[i])) {
               LChars = LChars + 1;
            }
            if (Char.IsDigit(arr[i])) {
               Nums = Nums + 1;
            }
            if (Char.IsWhiteSpace(arr[i])){
                Space = Space + 1;
            }
        }
        
        return (SChars, UChars, LChars, Nums, Space);

        }
    }
}
