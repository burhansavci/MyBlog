using MyBlog.Core.Entities;
using System;

namespace MyBlog.Entities.Dtos
{
    public class CategoryDto : IDto
    {
        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
