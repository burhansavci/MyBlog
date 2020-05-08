using MyBlog.Core.Entities;
using System.Collections.Generic;

namespace MyBlog.Entities.Dtos
{
    public class ArticleForArchiveReturnDto : IDto
    {
        public List<ArticleForArchiveDto> ArticleForArchiveDtos { get; set; }
        public int PublishYear { get; set; }
        public int PublishMonth { get; set; }
        public string MonthName { get; set; }
        public int CountByMonth { get; set; }
        public override bool Equals(object other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (this.GetType() != other.GetType()) return false;
            return Equals((ArticleForArchiveReturnDto)other);
        }

        public override int GetHashCode()
        {
            return PublishMonth.GetHashCode() + PublishYear.GetHashCode();
        }

        public bool Equals(ArticleForArchiveReturnDto other)
        {
            var result = other.PublishYear == PublishYear && other.PublishMonth == PublishMonth;
            return result;
        }
    }
}


