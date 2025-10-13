using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cinetheque.Models;
using cinetheque.Models.DAO;
using cinetheque.Models.DTO;

namespace cinetheque.Services
{
    public class LocationService : ILocationService
    {

        public List<LocationDto> getUserLocations(int id)
        {

            List<LocationDto> locations = new LocationDao().selectLocationById(id);

            return locations;
        }

        public int rentArticles(List<ArticleDto> articles)
        {
            int requestResult = 0;

            if (articles == null || articles.Count == 0)
                throw new ArgumentException("La liste d'articles est vide.");

            var session = HttpContext.Current?.Session;

            if (session == null || session["id"] == null ||session["login"] == null)
                throw new InvalidOperationException("Utilisateur non connecté.");

            int userId = (int)session["id"];

            try
            {
                foreach (var article in articles)
                {
                    double totalPrice = calculeTotalPrice(articles.Count, article.Price);
                    int qtyArticles = articles.Count;

                    requestResult = new Models.DAO.LocationDao().RentArticle(article, userId, totalPrice, qtyArticles);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erreur lors de la location des articles.", ex);
            }

            return requestResult;
        }

        private double calculeTotalPrice(int qtyArticles, double totalPrice)
        {
            double totalPriceCalcul = qtyArticles * totalPrice;
            return totalPriceCalcul;
        }
    }
}