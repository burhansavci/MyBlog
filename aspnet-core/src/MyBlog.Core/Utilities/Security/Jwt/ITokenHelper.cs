using MyBlog.Core.Entities.Concrete;

namespace MyBlog.Core.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {
        AccessToken CreateAccessToken(BaseUser user);
    }
}
