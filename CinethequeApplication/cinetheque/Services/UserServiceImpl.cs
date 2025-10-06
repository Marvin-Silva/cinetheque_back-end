using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cinetheque.Models;

namespace cinetheque.Services
{
    public class UserServiceImpl : IUserService
    {
        public void createUser(User user)
        {
            new UserDAO().InsertUser(user);
        }

        public bool login(User user)
        {
            return new UserDAO().SelectUser(user);
        }
    }
}