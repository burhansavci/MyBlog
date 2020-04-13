using MyBlog.Core.Entities;
using System;

namespace MyBlog.Entities.Dtos
{
    public class ArticleDto : IDto
    {
        public string Title { get; set; }
        public string ContentSummary { get; set; }
        public string ContentMain { get; set; }
        public DateTime PublishDate { get; set; }
        public int ViewCount { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public int? ArticleId { get; set; }
        public string LanguageCode { get; set; }

    }
}
