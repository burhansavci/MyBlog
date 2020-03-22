using System.Collections.Generic;

namespace MyBlog.Entities.Concrete
{
    public class Language : Entity
    {
        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }

        public virtual ICollection<ArticleTranslation> ArticleTranslation { get; set; }
        public virtual ICollection<CategoryTranslation> CategoryTranslation { get; set; }
    }
}
