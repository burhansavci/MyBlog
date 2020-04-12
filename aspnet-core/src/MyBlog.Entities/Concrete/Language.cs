using MyBlog.Core.Entities;
using System.Collections.Generic;

namespace MyBlog.Entities.Concrete
{
    public class Language : IEntity
    {
        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual ICollection<ArticleTranslation> ArticleTranslations { get; set; }
        public virtual ICollection<CategoryTranslation> CategoryTranslations { get; set; }
    }
}
