using MyBlog.Core.Entities;
using System;

namespace MyBlog.Entities.Dtos
{
    public class CategoryForReturnDto : IDto
    {
        public int Id { get; set; }
        public int CategoryTranslationId { get; set; }
        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
