using cinetheque.Models;
using cinetheque.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace cinetheque.Controllers
{
    [RoutePrefix("api/v1")]
    public class ArticleController : ApiController
    {
        private IArticleService _articleSrv;
        ArticleController()
        {
            _articleSrv = new ArticleService();
        }

        [Route("get/list")]
        [HttpGet]
        public List<Articles> articles()
        {
            return _articleSrv.getArticles();
        }

        [Route("get/article/{id}")]
        [HttpGet]
        public Articles articles([FromUri]int id)
        {
            return _articleSrv.getArticleById(id);
        }
    }
}
