using cinetheque.Models;
using cinetheque.Models.DTO;
using cinetheque.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace cinetheque.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [RoutePrefix("api/v1")]
    public class ArticleController : ApiController
    {
        private IArticleService _articleSrv;

        ArticleController()
        {
            this._articleSrv = new ArticleService();
        }

        [Route("get/list")]
        [HttpGet]
        public List<ArticleDto> articles()
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
