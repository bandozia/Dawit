using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dawit.Infrastructure.Service.Extensions
{
    public static class HashingExtensions
    {
        public static string ToHashed(this string input)
        {
            var salt = new byte[16];
            byte[] hash;
            var hashBytes = new byte[36];

            using (var rngProvider = new RNGCryptoServiceProvider())
                rngProvider.GetBytes(salt);

            using (var pbk = new Rfc2898DeriveBytes(input, salt, 10000))
                hash = pbk.GetBytes(20);

            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }

        public static bool IsEqualsHashed(this string input, string hashedInput)
        {
            var passBuffer = Convert.FromBase64String(hashedInput);
            var salt = new byte[16];
            var hash = new byte[32];

            Array.Copy(passBuffer, 0, salt, 0, 16);

            using (var pbk = new Rfc2898DeriveBytes(input, salt, 10000))
                hash = pbk.GetBytes(20);

            for (int i = 0; i < 20; i++)
                if (passBuffer[i + 16] != hash[i])
                    return false;

            return true;            
        }
    }
}
