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
            string insertSql = @"Data Source=cinesrv.database.windows.net;Initial Catalog=cinethequeBDD;User ID=test;Password=cine1234!;Encrypt=True;TrustServerCertificate=True;";

            //string insertSql = "INSERT INTO Locations (prix_total, qte_articles, utilisateur_id, article_id, date_debut, date_fin) VALUES (@prixTotal, @qteArticles, @userId, @articleId, @date_debut, @date_fin);";
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
        public List<Locations> selectLocationById(int id)
        {
            List<Locations> locations = new List<Locations>();
            string connectionString = @"Data Source=cinesrv.database.windows.net;Initial Catalog=cinethequeBDD;User ID=test;Password=cine1234!;Encrypt=True;TrustServerCertificate=True;";
            string sql = "SELECT * FROM locations WHERE utilisateur_id=@id"; ;

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Locations location = new Locations
                {
                    getId = Convert.ToInt32(reader["id"]),
                    getTotalPrice = (double)reader["prix_total"],
                    getQuantity = Convert.ToInt32(reader["qte_articles"]),
                    getStartDate = reader["date_debut"]!= DBNull.Value ? Convert.ToDateTime(reader["date_debut"]): DateTime.MinValue,
                    getEndDate = reader["date_fin"] != DBNull.Value ? Convert.ToDateTime(reader["date_fin"]) : DateTime.MinValue,
                    getArticleId = Convert.ToInt32(reader["article_id"]),
                    getUserId = Convert.ToInt32(reader["utilisateur_id"])
                };
                locations.Add(location);
                connection.Close();

                return locations;
            }
            return locations;
        }
    }
}