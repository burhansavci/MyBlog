using MyBlog.Core.DataAccess;
using MyBlog.Entities.Concrete;

namespace MyBlog.DataAccess.Abstract
{
    public interface IArticleRepository : IRepository<Article>
    {
        void SoftDelete(Article article);
    }
}
