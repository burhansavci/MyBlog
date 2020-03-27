using Microsoft.EntityFrameworkCore;
using MyBlog.Core.DataAccess.EntityFrameworkCore;
using MyBlog.DataAccess.Abstract;
using MyBlog.Entities.Concrete;

namespace MyBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfCorePictureRepository : EfCoreRepositoryBase<Picture, MyBlogDbContext>, IPictureRepository
    {
        public EfCorePictureRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
