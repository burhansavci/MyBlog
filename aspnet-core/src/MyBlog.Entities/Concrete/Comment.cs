using System;
using System.Collections.Generic;

namespace MyBlog.Entities.Concrete
{
    public class Comment : Entity
    {
        public string Name { get; set; }
        public string ContentMain { get; set; }
        public DateTime PublishDate { get; set; }

        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
        public int? ParentId { get; set; }
        public virtual Comment Parent { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
