using System.Security.Cryptography;
using System.Text;

namespace MyBlog.Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                if (computedHash.Length != passwordHash.Length)
                    return false;

                for (int i = 0; i < computedHash.Length; i++)
                    if (passwordHash[i] != computedHash[i])
                        return false;
            }
            return true;
        }
    }
}
