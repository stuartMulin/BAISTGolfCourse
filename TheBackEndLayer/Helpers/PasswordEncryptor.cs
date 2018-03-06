using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TheBackEndLayer.Helpers
{
    public static class PasswordEncryptor
    {
        #region Public Methods
        public static string CreateSalt(int size)
        {
            var rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }

        public static string CreatePasswordHash(string password, string salt)
        {
            var saltAndPassword = string.Concat(password, salt);

            var passwordHash = string.Join("",
            new MD5CryptoServiceProvider()
                .ComputeHash(
                new MemoryStream(Encoding.UTF8.GetBytes(saltAndPassword)))
                .Select(x => x.ToString("X2")));

            return passwordHash;
        }

        public static bool ValidateUserPassword(string suppliedPassword, string salt,
            string userPassword)
        {
            var hashedPasswordAndSalt = CreatePasswordHash(suppliedPassword, salt);

            return hashedPasswordAndSalt.Equals(userPassword);
        }
        #endregion
    }
}
