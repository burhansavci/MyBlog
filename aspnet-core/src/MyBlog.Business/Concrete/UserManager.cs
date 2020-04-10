using MyBlog.Business.Abstract;
using MyBlog.Business.Constants;
using MyBlog.Core.Utilities.Results;
using MyBlog.DataAccess.Abstract;
using MyBlog.Entities.Concrete;

namespace MyBlog.Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserRepository _userRepository;
        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IDataResult<User> InsertUser(User user)
        {
            _userRepository.Insert(user);
            return new SuccessDataResult<User>(Messages.SuccessfulInsert, user);
        }

        public IDataResult<User> GetByEmail(string email)
        {
            return new SuccessDataResult<User>(Messages.SuccessOperation, _userRepository.Get(x => x.Email == email));
        }

        public IDataResult<User> GetByUserName(string userName)
        {
            return new SuccessDataResult<User>(Messages.SuccessOperation, _userRepository.Get(x => x.UserName == userName));
        }
    }
}
