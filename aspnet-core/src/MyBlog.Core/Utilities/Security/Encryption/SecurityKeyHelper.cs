using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MyBlog.Core.Utilities.Security.Encryption
{
    public class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securtiyKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securtiyKey));
        }
    }
}
