using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cinetheque.Models;
using cinetheque.Models.DAO;

namespace cinetheque.Services
{
    public class ArticleService : IArticleService
    {
        public Articles getArticleById(int id)
        {
            return new ArticleDao().selectById(id);
        }

        public List<Articles> getArticles()
        {
            return new ArticleDao().selectAll();
        }
    }
}