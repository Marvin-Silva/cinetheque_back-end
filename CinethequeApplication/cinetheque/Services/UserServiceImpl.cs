using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cinetheque.Models;
using cinetheque.Models.DAO;

namespace cinetheque.Services
{
    public class UserServiceImpl : IUserService
    {
        private ArticleService articleSrv;

        public UserServiceImpl()
        {
            this.articleSrv = new ArticleService();
        }

        public void createUser(UserDto user)
        {
            new UserDAO().InsertUser(user);
        }

        public List<Articles> getArticleList(int id)
        {
            return this.articleSrv.getArticleList(id);
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