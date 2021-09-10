using System;
using System.Security.Cryptography;
using UnityEngine;

namespace Stoicode.UniLib.Security
{
    public static class Pbkdf2Hasher
    {
        private const int SaltSize = 24;
        private const int HashSize = 20;
        private const int HashIterations = 25000;

        private static readonly RNGCryptoServiceProvider crypto =
            new RNGCryptoServiceProvider();


        /// <summary>
        /// Hash string
        /// </summary>
        /// <param name="target">Target string</param>
        /// <returns>Hashed string</returns>
        public static string Hash(string target)
        {
            var salt = new byte[SaltSize];
            crypto.GetBytes(salt);

            var pbkdf2 = new Rfc2898DeriveBytes(target, salt)
            {
                IterationCount = HashIterations
            };

            var hash = pbkdf2.GetBytes(HashSize);

            return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
        }

        /// <summary>
        /// Validate hash equality
        /// </summary>
        /// <param name="target">Target string</param>
        /// <param name="compare">Hash to compare with</param>
        /// <returns>Hash equality validity</returns>
        public static bool Validate(string target, string compare)
        {
            var split = compare.Split(':');
            var salt = Convert.FromBase64String(split[0]);
            var hash = Convert.FromBase64String(split[1]);

            var pbkdf2 = new Rfc2898DeriveBytes(target, salt)
            {
                IterationCount = HashIterations
            };
            
            var testHash = pbkdf2.GetBytes(HashSize);

            return SlowEquals(hash, testHash);
        }

        /// <summary>
        /// Slow equals compare hashes
        /// </summary>
        /// <param name="a">Hash</param>
        /// <param name="b">Hash</param>
        /// <returns>Hash comparison validation</returns>
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint)a.Length ^ (uint)b.Length;
            for (var i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            
            return diff == 0;
        }
    }
}