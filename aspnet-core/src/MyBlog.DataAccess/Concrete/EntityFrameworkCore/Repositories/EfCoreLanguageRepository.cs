using Microsoft.EntityFrameworkCore;
using MyBlog.Core.DataAccess.EntityFrameworkCore;
using MyBlog.DataAccess.Abstract;
using MyBlog.Entities.Concrete;

namespace MyBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfCoreLanguageRepository : EfCoreRepositoryBase<Language, MyBlogDbContext>, ILanguageRepository
    {
        public EfCoreLanguageRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
