using Microsoft.EntityFrameworkCore;
using MyBlog.Core.DataAccess.EntityFrameworkCore;
using MyBlog.DataAccess.Abstract;
using MyBlog.Entities.Concrete;

namespace MyBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfCoreArticleRepository : EfCoreRepositoryBase<Article, MyBlogDbContext>, IArticleRepository
    {
        public EfCoreArticleRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public void SoftDelete(Article article)
        {
            article.IsActive = false;
            _dbContext.Entry(article).Property(c => c.IsActive).IsModified = true;
            _dbContext.SaveChanges();
        }
    }
}
