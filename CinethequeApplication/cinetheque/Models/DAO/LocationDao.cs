using cinetheque.Models.DTO;
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
        public List<LocationDto> selectLocationById(int id)
        {
            List<LocationDto> locations = new List<LocationDto>();
            //string connectionString = @"Data Source=cinesrv.database.windows.net;Initial Catalog=cinethequeBDD;User ID=test;Password=cine1234!;Encrypt=True;TrustServerCertificate=True;";
            string connectionString = @"Data Source=LAPTOP-R6OUBGEG;Initial Catalog=cinethequeDB;User ID=marvin;Password=Soleil.123";

            string sql = @"SELECT l.id AS location_id,
                                    l.prix_total,
                                    l.qte_articles,
                                    l.date_debut,
                                    l.date_fin,

                                    a.id AS article_id,
                                    a.nom AS article_nom,
                                    a.prix AS article_prix,
                                    a.description,
                                    a.qte_totale,
                                    a.qte_dispo,

                                    c.id AS category_id,
                                    c.categorie AS category_nom,

                                    u.nom AS user_nom,
                                    u.prenom AS user_prenom

                                FROM locations l
                                INNER JOIN articles a ON l.article_id = a.id
                                LEFT JOIN categories c ON a.categorie_id = c.id
                                LEFT JOIN utilisateur_infos u ON l.utilisateur_id = u.utilisateur_id
                                WHERE l.utilisateur_id = 11"; ;


            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                LocationDto location = new LocationDto
                {
                    Id = Convert.ToInt32(reader["location_id"]),
                    TotalPrice = Convert.ToDouble(reader["prix_total"]),
                    StartDate = reader["date_debut"] != DBNull.Value ? Convert.ToDateTime(reader["date_debut"]) : DateTime.MinValue,
                    EndDate = reader["date_fin"] != DBNull.Value ? Convert.ToDateTime(reader["date_fin"]) : DateTime.MinValue,

                    Article = new ArticleDto
                    {
                        Id = Convert.ToInt32(reader["article_id"]),
                        Price = Convert.ToDouble(reader["article_prix"]),
                        Name = reader["article_nom"].ToString(),
                        Description = reader["description"].ToString(),
                        TotalQty = Convert.ToInt32(reader["qte_totale"]),
                        DispoQty = Convert.ToInt32(reader["qte_dispo"]),
                        Category = new Category
                        {
                            Id = Convert.ToInt32(reader["category_id"]),
                            CategoryName = reader["category_nom"].ToString()
                        }
                    },

                    User = new UserDto
                    {
                        getUserName = reader["user_nom"].ToString(),
                        getUserFirstName = reader["user_prenom"].ToString()
                    }
                };

                locations.Add(location);
            }

            return locations;
        }
    }
}