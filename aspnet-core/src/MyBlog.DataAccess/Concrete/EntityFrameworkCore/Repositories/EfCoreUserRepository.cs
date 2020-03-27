using Microsoft.EntityFrameworkCore;
using MyBlog.Core.DataAccess.EntityFrameworkCore;
using MyBlog.DataAccess.Abstract;
using MyBlog.Entities.Concrete;

namespace MyBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfCoreUserRepository : EfCoreRepositoryBase<User, MyBlogDbContext>, IUserRepository
    {
        public EfCoreUserRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
