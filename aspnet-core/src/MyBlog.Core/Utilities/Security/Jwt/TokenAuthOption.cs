using System;

namespace MyBlog.Core.Utilities.Security.Jwt
{
    public class TokenAuthOption
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string SecurityKey { get; set; }
        public int ExpirationTime { get; set; }
    }
}
