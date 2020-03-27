using Microsoft.EntityFrameworkCore;
using MyBlog.Core.DataAccess.EntityFrameworkCore;
using MyBlog.DataAccess.Abstract;
using MyBlog.Entities.Concrete;

namespace MyBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfCoreCommentRepository : EfCoreRepositoryBase<Comment, MyBlogDbContext>, ICommentRepository
    {
        public EfCoreCommentRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
