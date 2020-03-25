using MyBlog.Core.DataAccess;
using MyBlog.Entities.Concrete;

namespace MyBlog.DataAccess.Abstract
{
    public interface IArticleTranslationRepository : IRepository<ArticleTranslation>
    {
    }
}
