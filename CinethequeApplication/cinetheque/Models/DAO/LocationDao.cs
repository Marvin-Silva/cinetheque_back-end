using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace cinetheque.Models.DAO
{
    public class LocationDao
    {
        public int RentArticle(Articles article, int userId, double totalPrice, int qtyArticles)
        {
            int resultFirstRequest = 0;
            int resultSecondRequest = 0;

            string connectionString = @"Data Source=cinesrv.database.windows.net;Initial Catalog=cinethequeBDD;User ID=test;Password=cine1234!;Encrypt=True;TrustServerCertificate=True;";

            SqlConnection connection = new SqlConnection(connectionString);
            
            connection.Open();

            string insertSql = "INSERT INTO Locations (prix_total, qte_articles, utilisateur_id, article_id, date_debut, date_fin) VALUES (@prixTotal, @qteArticles, @userId, @articleId, @date_debut, @date_fin);";
            SqlCommand command = new SqlCommand(insertSql, connection);

            Locations location = new Locations();

            location.getUserId = userId;
            location.getArticleId = article.getId;
            location.getQuantity = qtyArticles;
            location.getStartDate = DateTime.Now;
            location.getEndDate = DateTime.Now.AddMonths(1);


            command.Parameters.AddWithValue("@userId", location.getUserId);
            command.Parameters.AddWithValue("@articleId", location.getArticleId);
            command.Parameters.AddWithValue("@prixTotal", totalPrice);
            command.Parameters.AddWithValue("@qteArticles", location.getQuantity);
            command.Parameters.AddWithValue("@date_debut", location.getStartDate);
            command.Parameters.AddWithValue("@date_fin", location.getEndDate);


            resultFirstRequest = command.ExecuteNonQuery();
                

            string updateArticle = "UPDATE Articles SET qte_dispo = qte_dispo - 1 WHERE id = @id AND qte_dispo > 0;";
            SqlCommand commandUpdate = new SqlCommand(updateArticle, connection);
                
            commandUpdate.Parameters.AddWithValue("@id", article.getId);
            resultSecondRequest = commandUpdate.ExecuteNonQuery();

            connection.Close();

            if(resultFirstRequest > 0 && resultSecondRequest > 0)
            {
                int finalResult = resultFirstRequest + resultSecondRequest;
                Console.WriteLine("Location enregistrée avec succès !");
                return finalResult;
            }else
            {
                return 0;
            }
        }
    }
}