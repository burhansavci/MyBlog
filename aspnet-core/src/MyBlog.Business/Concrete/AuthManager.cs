using MyBlog.Business.Abstract;
using MyBlog.Business.Constants;
using MyBlog.Core.Utilities.Results;
using MyBlog.Core.Utilities.Security.Hashing;
using MyBlog.Core.Utilities.Security.Jwt;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        IUserService _userService;
        ITokenHelper _tokenHelper;
        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }
        public IDataResult<User> Login(UserLoginDto userLoginDto)
        {
            var userToCheck = _userService.GetByEmail(userLoginDto.Email);
            if (userToCheck.Data == null)
                return new ErrorDataResult<User>(Messages.UserNotFound);

            if (!HashingHelper.VerifyPasswordHash(userLoginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
                return new ErrorDataResult<User>(Messages.PasswordError);

            return new SuccessDataResult<User>(Messages.SuccessfulLogin, userToCheck.Data);
        }

        public IDataResult<User> Register(UserRegisterDto userForRegisterDto)
        {
            if (!UserAlreadyExists(userForRegisterDto.Email, userForRegisterDto.Username).Success)
                return new ErrorDataResult<User>(Messages.UserAlreadyExists);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Username = userForRegisterDto.Username,
                Email = userForRegisterDto.Email,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash,
            };
            _userService.InsertUser(user);

            return new SuccessDataResult<User>(Messages.UserRegistered, user);

        }

        public IResult UserAlreadyExists(string email, string userName)
        {
            if (_userService.GetByEmail(email).Data != null)
                return new ErrorResult(Messages.UserAlreadyExists);

            if (_userService.GetByUserName(userName).Data != null)
                return new ErrorResult(Messages.UserAlreadyExists);

            return new SuccessResult();
        }
        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            return new SuccessDataResult<AccessToken>(Messages.AccessTokenCreated, _tokenHelper.CreateAccessToken(user));
        }
    }
}
