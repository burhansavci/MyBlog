using System.Collections.Generic;

namespace MyBlog.Entities.Concrete
{
    public class User : Entity
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public ICollection<Article> Articles { get; set; }

    }
}
