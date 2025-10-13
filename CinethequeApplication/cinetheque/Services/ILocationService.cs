using cinetheque.Models;
using cinetheque.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cinetheque.Services
{
    interface ILocationService
    {
        int rentArticles(List<ArticleDto> article);
        List<LocationDto> getUserLocations(int id);
    }
}
