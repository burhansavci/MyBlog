using Microsoft.EntityFrameworkCore;
using MyBlog.Core.DataAccess.EntityFrameworkCore;
using MyBlog.DataAccess.Abstract;
using MyBlog.Entities.Concrete;

namespace MyBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfCoreArticleTranslationRepository : EfCoreRepositoryBase<ArticleTranslation, MyBlogDbContext>, IArticleTranslationRepository
    {
        public EfCoreArticleTranslationRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
