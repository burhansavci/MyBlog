using Microsoft.EntityFrameworkCore;
using MyBlog.Core.DataAccess.EntityFrameworkCore;
using MyBlog.DataAccess.Abstract;
using MyBlog.Entities.Concrete;

namespace MyBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfCoreCategoryRepository : EfCoreRepositoryBase<Category, MyBlogDbContext>, ICategoryRepository
    {
        public EfCoreCategoryRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
