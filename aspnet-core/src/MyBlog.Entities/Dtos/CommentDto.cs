using MyBlog.Core.Entities;
using System;

namespace MyBlog.Entities.Dtos
{
    public class CommentDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContentMain { get; set; }
        public DateTime PublishDate { get; set; }
        public int ArticleId { get; set; }
        public int? ParentId { get; set; }
    }
}
