using MyBlog.Core.Entities;
using System;
using System.Collections.Generic;

namespace MyBlog.Entities.Dtos
{
    public class ArticleForReturnDto : IDto
    {
        public int Id { get; set; }
        public int ArticleTranslationId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string ContentSummary { get; set; }
        public string ContentMain { get; set; }
        public DateTime PublishDate { get; set; }
        public int ViewCount { get; set; }
        public string LanguageCode { get; set; }
        public CategoryForReturnDto Category { get; set; }
        public PictureForReturnDto Picture { get; set; }
    }
}
