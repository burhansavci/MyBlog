using Microsoft.EntityFrameworkCore;
using MyBlog.Core.DataAccess.EntityFrameworkCore;
using MyBlog.DataAccess.Abstract;
using MyBlog.Entities.Concrete;

namespace MyBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class CategoryTranslationRepository : EfCoreRepositoryBase<CategoryTranslation, MyBlogDbContext>, ICategoryTranslationRepository
    {
        public CategoryTranslationRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
