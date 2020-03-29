using MyBlog.Core.Entities.Concrete;
using System.Collections.Generic;

namespace MyBlog.Entities.Concrete
{
    public class User : BaseUser
    {
        public ICollection<Article> Articles { get; set; }

    }
}
