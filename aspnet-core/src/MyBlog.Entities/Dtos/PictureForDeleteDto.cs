using MyBlog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Entities.Dtos
{
    public class PictureForDeleteDto : IDto
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public string PublicId { get; set; }
    }
}
