using cinetheque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cinetheque.Services
{
    interface IArticleService
    {
        List<Articles> getArticles();
        Articles getArticleById(int id);
    }
}
