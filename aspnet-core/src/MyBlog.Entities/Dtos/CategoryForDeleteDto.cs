using MyBlog.Core.Entities;

namespace MyBlog.Entities.Dtos
{
    public class CategoryForDeleteDto : IDto
    {
        public int Id { get; set; }
        public int CategoryTranslationId { get; set; }
    }
}
