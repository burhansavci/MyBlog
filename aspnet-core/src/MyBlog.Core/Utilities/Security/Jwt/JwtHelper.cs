using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyBlog.Core.Entities.Concrete;
using MyBlog.Core.Utilities.Security.Encryption;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MyBlog.Core.Utilities.Security.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private TokenAuthOption _tokenAuthOptions;
        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenAuthOptions = Configuration.GetSection("TokenAuthOption").Get<TokenAuthOption>();
        }
        public AccessToken CreateAccessToken(BaseUser user)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenAuthOptions.ExpirationTime);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenAuthOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenAuthOptions, signingCredentials, user);
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenAuthOption tokenAuthOptions,
                                                       SigningCredentials signingCredentials,
                                                       BaseUser user)
        {

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };

            return new JwtSecurityToken(
                issuer: tokenAuthOptions.Issuer,
                audience: tokenAuthOptions.Audience,
                notBefore: DateTime.Now,
                expires: _accessTokenExpiration,
                signingCredentials: signingCredentials,
                claims: claims
                );
        }

    }
}
