using Microsoft.EntityFrameworkCore;
using MyBlog.Core.DataAccess.EntityFrameworkCore;
using MyBlog.DataAccess.Abstract;
using MyBlog.Entities.Concrete;

namespace MyBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class ArticleTranslationRepository : EfCoreRepositoryBase<ArticleTranslation, MyBlogDbContext>, IArticleTranslationRepository
    {
        public ArticleTranslationRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
