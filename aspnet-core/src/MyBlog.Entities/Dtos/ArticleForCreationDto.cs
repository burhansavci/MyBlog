using MyBlog.Core.Entities;
using System;
using System.Collections.Generic;

namespace MyBlog.Entities.Dtos
{
    public class ArticleForCreationDto : IDto
    {
        public string Title { get; set; }
        public string ContentSummary { get; set; }
        public string ContentMain { get; set; }
        public DateTime PublishDate { get; set; }
        public List<PictureForCreationDto> Pictures { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public string LanguageCode { get; set; }

    }
}
