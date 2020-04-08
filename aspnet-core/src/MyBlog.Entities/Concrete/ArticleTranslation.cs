using MyBlog.Core.Entities;

namespace MyBlog.Entities.Concrete
{
    public class ArticleTranslation : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ContentSummary { get; set; }
        public string ContentMain { get; set; }

        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
        public string LanguageCode { get; set; }
        public virtual Language Language { get; set; }
    }
}
