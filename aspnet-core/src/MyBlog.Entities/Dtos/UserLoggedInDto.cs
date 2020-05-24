using MyBlog.Core.Entities;
using System;

namespace MyBlog.Entities.Dtos
{
    public class UserLoggedInDto : IDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
