namespace MyBlog.Entities.Concrete
{
    public class ArticleTranslation : Entity
    {
        public string Title { get; set; }
        public string ContentSummary { get; set; }
        public string ContentMain { get; set; }

        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
    }
}
