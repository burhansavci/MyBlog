using MyBlog.Core.Entities;
using System.Collections.Generic;

namespace MyBlog.Entities.Dtos
{
    public class ArticleForArchiveMonthDto : IDto
    {
        public List<ArticleForArchiveDto> ArticleArchives { get; set; }
        public int PublishMonth { get; set; }
        public string MonthName { get; set; }
        public int CountByMonth { get; set; }
        public override bool Equals(object other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (this.GetType() != other.GetType()) return false;
            return Equals((ArticleForArchiveMonthDto)other);
        }

        public override int GetHashCode()
        {
            return PublishMonth.GetHashCode();
        }

        public bool Equals(ArticleForArchiveMonthDto other)
        {
            var result = other.PublishMonth == PublishMonth;
            return result;
        }
    }
}
