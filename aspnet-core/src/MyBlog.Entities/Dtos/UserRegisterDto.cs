using MyBlog.Core.Entities;

namespace MyBlog.Entities.Dtos
{
    public class UserRegisterDto : IDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
