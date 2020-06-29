using MyBlog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Entities.Dtos
{
    public class ArticleForDeleteDto : IDto
    {
        public int ArticleTranslationId { get; set; }
        public int Id { get; set; }
    }
}
