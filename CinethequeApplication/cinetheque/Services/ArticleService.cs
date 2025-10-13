using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cinetheque.Models;
using cinetheque.Models.DAO;
using cinetheque.Models.DTO;

namespace cinetheque.Services
{
    public class ArticleService : IArticleService
    {
        private LocationService locationSrv;

        public ArticleService()
        {
            locationSrv = new LocationService();
        }

        public Articles getArticleById(int id)
        {
            return new ArticleDao().selectById(id);
        }

        public List<ArticleDto> getArticles()
        {
            return new ArticleDao().selectAll();
        }
    }
}