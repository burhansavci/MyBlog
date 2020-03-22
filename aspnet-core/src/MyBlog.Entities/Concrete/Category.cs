using System.Collections.Generic;

namespace MyBlog.Entities.Concrete
{
    public class Category : Entity
    {
        public virtual ICollection<Article> Article { get; set; }
        public virtual ICollection<CategoryTranslation> CategoryTranslation { get; set; }
    }
}
