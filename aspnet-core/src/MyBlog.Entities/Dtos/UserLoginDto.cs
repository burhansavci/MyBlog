﻿using MyBlog.Core.Entities;

namespace MyBlog.Entities.Dtos
{
    public  class UserLoginDto:IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
