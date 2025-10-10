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
        private LocationService locationSrv;

        public ArticleService()
        {
            locationSrv = new LocationService();
        }

        public Articles getArticleById(int id)
        {
            return new ArticleDao().selectById(id);
        }

        public List<Articles> getArticles()
        {
            return new ArticleDao().selectAll();
        }

        public List<Articles> getArticleList(int id)
        {
            List<Locations> locations = this.locationSrv.getUserLocations(id);

            List<Articles> articles = new List<Articles>();

            foreach(Locations location in locations)
            {
                if (location != null && location.getUserId == id)
                {
                    articles = new ArticleDao().selectArticleByUserId(location.getArticleId);
                }
            }         
            return articles;
        }
    }
}