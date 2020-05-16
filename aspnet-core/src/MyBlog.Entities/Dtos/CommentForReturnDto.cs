using MyBlog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Entities.Dtos
{
    public class CommentForReturnDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContentMain { get; set; }
        public DateTime PublishDate { get; set; }
        public int ArticleId { get; set; }
        public int? ParentId { get; set; }
        public List<CommentForReturnDto> SubComments { get; set; }
    }
}
