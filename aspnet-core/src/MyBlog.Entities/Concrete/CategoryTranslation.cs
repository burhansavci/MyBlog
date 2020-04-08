﻿using MyBlog.Core.Entities;

namespace MyBlog.Entities.Concrete
{
    public class CategoryTranslation : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string LanguageCode { get; set; }
        public virtual Language Language { get; set; }
    }
}
