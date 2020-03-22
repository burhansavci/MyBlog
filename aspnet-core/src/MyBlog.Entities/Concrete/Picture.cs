namespace MyBlog.Entities.Concrete
{
    public class Picture : Entity
    {
        public bool IsMain { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }

        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}
