using System.Security.Cryptography;
using System.Text;

namespace Stoicode.UniLib.Security
{
    public static class ShaHasher
    {
        public static string Hash(HashType type, string target)
        {
            byte[] hash;
            
            switch (type)
            {
                case HashType.Sha256:
                    var sha256 = SHA256.Create();
                    var bytes256 = Encoding.UTF8.GetBytes(target);
                    hash = sha256.ComputeHash(bytes256);
                    break;
                
                case HashType.Sha512:
                    var sha512 = SHA512.Create();
                    var bytes512 = Encoding.UTF8.GetBytes(target);
                    hash = sha512.ComputeHash(bytes512);
                    break;

                default:
                    return string.Empty;
            }
            
            return GetStringFromHash(hash);
        }
        
        private static string GetStringFromHash(byte[] hash)
        {
            var result = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
                result.Append(hash[i].ToString("X2"));
            
            return result.ToString();
        }
    }
}