using MyBlog.Core.Entities;

namespace MyBlog.Entities.Dtos
{
    public class ArticleForArchiveReturnDto : IDto
    {
        public string Title { get; set; }
        public int Id { get; set; }
        public int PublishYear { get; set; }
        public int PublishMonth { get; set; }
        public string MonthName { get; set; }
        public int CountByMonth { get; set; }
    }
}
