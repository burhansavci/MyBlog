using MyBlog.Core.Entities;

namespace MyBlog.Entities.Dtos
{
    public class LanguageDto : IDto
    {
        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
    }
}
