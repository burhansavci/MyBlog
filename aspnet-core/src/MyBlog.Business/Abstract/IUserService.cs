using MyBlog.Core.Utilities.Results;
using MyBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Business.Abstract
{
    public interface IUserService
    {
        IDataResult<User> GetByEmail(string email);
        IDataResult<User> InsertUser(User user);
    }
}
