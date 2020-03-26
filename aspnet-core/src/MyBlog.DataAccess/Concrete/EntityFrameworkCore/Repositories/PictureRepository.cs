using Microsoft.EntityFrameworkCore;
using MyBlog.Core.DataAccess.EntityFrameworkCore;
using MyBlog.DataAccess.Abstract;
using MyBlog.Entities.Concrete;

namespace MyBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class PictureRepository : EfCoreRepositoryBase<Picture, MyBlogDbContext>, IPictureRepository
    {
        public PictureRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
