using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cinetheque.Models;

namespace cinetheque.Services
{
    public class UserServiceImpl : IUserService
    {
        public void createUser(UserDto user)
        {
            new UserDAO().InsertUser(user);
        }

        public UserInfo getUserInfo(int id)
        {
            return new UserDAO().SelectUserInfo(id);
        }

        public bool login(User user)
        {
            return new UserDAO().SelectUser(user);
        }
    }
}