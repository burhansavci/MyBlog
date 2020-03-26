using Microsoft.EntityFrameworkCore;
using MyBlog.Core.DataAccess.EntityFrameworkCore;
using MyBlog.DataAccess.Abstract;
using MyBlog.Entities.Concrete;

namespace MyBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class LanguageRepository : EfCoreRepositoryBase<Language, MyBlogDbContext>, ILanguageRepository
    {
        public LanguageRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
