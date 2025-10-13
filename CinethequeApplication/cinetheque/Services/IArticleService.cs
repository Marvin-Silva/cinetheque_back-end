using cinetheque.Models;
using cinetheque.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cinetheque.Services
{
    interface IArticleService
    {
        List<ArticleDto> getArticles();
        Articles getArticleById(int id);
    }
}
