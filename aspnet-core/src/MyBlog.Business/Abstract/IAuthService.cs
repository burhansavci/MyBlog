using MyBlog.Core.Utilities.Results;
using MyBlog.Core.Utilities.Security.Jwt;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;

namespace MyBlog.Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserRegisterDto userRegisterDto);
        IDataResult<User> Login(UserLoginDto userLoginDto);
        IResult UserAlreadyExists(string email, string userName);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
