using MyBlog.Core.Entities.Concrete;

namespace MyBlog.Entities.Concrete
{
    public class CategoryTranslation : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
    }
}
