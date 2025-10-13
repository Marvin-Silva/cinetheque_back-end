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
        public int RentArticle(ArticleDto article, int userId, double totalPrice, int qtyArticles)
        {
            int result = 0;

            string connectionString = @"Data Source=cinesrv.database.windows.net;Initial Catalog=cinethequeBDD;User ID=test;Password=cine1234!;Encrypt=True;TrustServerCertificate=True;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Vérification que l'article existe
                    string checkArticleSql = "SELECT COUNT(*) FROM articles WHERE id = @articleId";
                    using (SqlCommand checkCommand = new SqlCommand(checkArticleSql, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@articleId", article.Id);
                        int articleExists = (int)checkCommand.ExecuteScalar();

                        if (articleExists == 0)
                        {
                            throw new Exception("L'article spécifié n'existe pas dans la base de données.");
                        }
                    }

                    // Insertion dans Locations
                    string insertSql = @"INSERT INTO Locations 
                (prix_total, qte_articles, utilisateur_id, article_id, date_debut, date_fin) 
                VALUES (@prixTotal, @qteArticles, @userId, @articleId, @date_debut, @date_fin);";

                    using (SqlCommand command = new SqlCommand(insertSql, connection))
                    {
                        Locations location = new Locations
                        {
                            getUserId = userId,
                            getArticleId = article.Id,
                            getQuantity = qtyArticles,
                            getStartDate = DateTime.Now,
                            getEndDate = DateTime.Now.AddMonths(1)
                        };

                        command.Parameters.AddWithValue("@userId", location.getUserId);
                        command.Parameters.AddWithValue("@articleId", location.getArticleId);
                        command.Parameters.AddWithValue("@prixTotal", totalPrice);
                        command.Parameters.AddWithValue("@qteArticles", location.getQuantity);
                        command.Parameters.AddWithValue("@date_debut", location.getStartDate);
                        command.Parameters.AddWithValue("@date_fin", location.getEndDate);

                        result = command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erreur SQL : " + ex.Message);
                // Vous pouvez aussi logger l'erreur ou la relancer
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
                throw;
            }

            return result;
        }

        public List<LocationDto> selectLocationById(int id)
        {
            List<LocationDto> locations = new List<LocationDto>();
            string connectionString = @"Data Source=cinesrv.database.windows.net;Initial Catalog=cinethequeBDD;User ID=test;Password=cine1234!;Encrypt=True;TrustServerCertificate=True;";
            //string connectionString = @"Data Source=LAPTOP-R6OUBGEG;Initial Catalog=cinethequeDB;User ID=marvin;Password=Soleil.123";

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
                                WHERE l.utilisateur_id =@id"; ;


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
                    Quantity = Convert.ToInt32(reader["qte_articles"]),
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