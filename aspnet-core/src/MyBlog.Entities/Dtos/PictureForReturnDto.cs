using MyBlog.Core.Entities;

namespace MyBlog.Entities.Dtos
{
    public class PictureForReturnDto : IDto
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
    }
}
