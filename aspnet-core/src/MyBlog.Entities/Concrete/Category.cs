using System;
using System.Collections.Generic;

namespace MyBlog.Entities.Concrete
{
    public class Category : Entity
    {
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<CategoryTranslation> CategoryTranslations { get; set; }
    }
}
