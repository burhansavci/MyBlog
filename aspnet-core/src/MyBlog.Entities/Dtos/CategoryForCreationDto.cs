using MyBlog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Entities.Dtos
{
    public class CategoryForCreationDto:IDto
    {
        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
