using MyBlog.Core.Entities;
using System;
using System.Collections.Generic;

namespace MyBlog.Entities.Dtos
{
    public class ArticleForUpdateDto : IDto
    {
        public string Title { get; set; }
        public string ContentSummary { get; set; }
        public string ContentMain { get; set; }
        public DateTime PublishDate { get; set; }
        public List<PictureForUpdateDto> Pictures { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public int ArticleTranslationId { get; set; }
        public int Id { get; set; }
        public string LanguageCode { get; set; }

    }
}
