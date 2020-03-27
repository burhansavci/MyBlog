using Microsoft.EntityFrameworkCore;
using MyBlog.Core.DataAccess.EntityFrameworkCore;
using MyBlog.DataAccess.Abstract;
using MyBlog.Entities.Concrete;

namespace MyBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfCoreCategoryTranslationRepository : EfCoreRepositoryBase<CategoryTranslation, MyBlogDbContext>, ICategoryTranslationRepository
    {
        public EfCoreCategoryTranslationRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
