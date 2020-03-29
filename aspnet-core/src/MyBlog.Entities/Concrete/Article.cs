using MyBlog.Core.Entities.Concrete;
using System;
using System.Collections.Generic;

namespace MyBlog.Entities.Concrete
{
    public class Article : Entity
    {
        public DateTime PublishDate { get; set; }
        public int ViewCount { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int UserId { get; set; }
        public  User User { get; set; }

        public  ICollection<ArticleTranslation> ArticleTranslations { get; set; }
        public  ICollection<Comment> Comments { get; set; }
        public  ICollection<Picture> Pictures { get; set; }
    }
}
