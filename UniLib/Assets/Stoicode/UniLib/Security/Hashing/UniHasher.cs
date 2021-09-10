using System;
using System.Text;

namespace Stoicode.UniLib.Security
{
    public static class UniHasher
    {
        public static string Hash(HashType type, string target)
        {
            return type switch
            {
                HashType.Sha256 => ShaHasher.Hash(type, target),
                HashType.Sha512 => ShaHasher.Hash(type, target),
                HashType.Pbkdf2 => Pbkdf2Hasher.Hash(target),
                _ => Convert.ToBase64String(Encoding.UTF8.GetBytes(target))
            };
        }

        public static bool Validate(HashType type, string target, string hash)
        {
            return type switch
            {
                HashType.Sha256 => ShaHasher.Hash(type, target).Equals(hash),
                HashType.Sha512 => ShaHasher.Hash(type, target).Equals(hash),
                HashType.Pbkdf2 => Pbkdf2Hasher.Validate(target, hash),
                _ => Convert.ToBase64String(Encoding.UTF8.GetBytes(target)).Equals(hash)
            };
        }
    }
}