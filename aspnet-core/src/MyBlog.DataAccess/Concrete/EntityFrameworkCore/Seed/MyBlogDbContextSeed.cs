using Microsoft.EntityFrameworkCore;
using MyBlog.Core.Utilities.Security.Hashing;
using MyBlog.Entities.Concrete;
using System.Threading.Tasks;

namespace MyBlog.DataAccess.Concrete.EntityFrameworkCore.Seed
{
    public static class MyBlogDbContextSeed
    {
        //You can change username,email and password
        public static async Task SeedUsersAsync(MyBlogDbContext context)
        {
            var admin = await context.Users.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.UserName == "myblogadmin" && x.Email == "myblogadminemail");
            if (admin == null)
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash("myblogadminpassword", out passwordHash, out passwordSalt);
                var user = new User
                {
                    UserName = "myblogadmin",
                    Email = "myblogadminemail",
                    PasswordSalt = passwordSalt,
                    PasswordHash = passwordHash,
                };
                await context.AddAsync(user);
                await context.SaveChangesAsync();
            }
        }
    }
}
