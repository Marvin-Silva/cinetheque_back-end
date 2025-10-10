using cinetheque.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace cinetheque.Models.DAO
{
    public class ArticleDao
    {
     public List<ArticleDto> selectAll() { 
        List<ArticleDto> articles = new List<ArticleDto>();
            string connectionString = @"Data Source=cinesrv.database.windows.net;Initial Catalog=cinethequeBDD;User ID=test;Password=cine1234!;Encrypt=True;TrustServerCertificate=True;";
            //string connectionString = @"Data Source=LAPTOP-R6OUBGEG;Initial Catalog=cinethequeDB;User ID=marvin;Password=Soleil.123";

            string sql = @"SELECT a.*, c.categorie
                   FROM articles a 
                   INNER JOIN categories c ON a.categorie_id = c.id";

        SqlConnection connection = new SqlConnection(connectionString);

        SqlCommand command = new SqlCommand(sql, connection);
        connection.Open();
        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {

                ArticleDto article = new ArticleDto
                {
                    Id = (int)reader["id"],
                    Name = reader["nom"].ToString(),
                    Price = Convert.ToDouble(reader["prix"]),
                    Description = reader["description"].ToString(),
                    TotalQty = (int)reader["qte_totale"],
                    DispoQty = (int)reader["qte_dispo"],
                    
                    Category = new Category {
                        Id = (int)reader["id"],
                        CategoryName = (string)reader["categorie"]
                    }
                };

                articles.Add(article);    
        }
            return articles;
        }

        public Articles selectById(int id)
        {
            Articles article = new Articles();
            string connectionString = @"Data Source=cinesrv.database.windows.net;Initial Catalog=cinethequeBDD;User ID=test;Password=cine1234!;Encrypt=True;TrustServerCertificate=True;";

            string sql = "SELECT * FROM Articles WHERE id=@id"; ;

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                article = new Articles
                {
                    getId = (int)reader["id"],
                    getPrice = (double)reader["prix"],
                    getName = reader["nom"].ToString(),
                    getCatId = (int)reader["categorie_id"],
                    getDescription = reader["description"].ToString(),
                    getTotalQty = (int)reader["qte_totale"],
                    getDispoQty = (int)reader["qte_dispo"]
                };
                connection.Close();

                return article;
            }
            return article;
        }

        //public List<Articles> selectArticleByUserId(int id)
        //{
        //    List<Articles> articles = new List<Articles>();
        //    //string connectionString = @"Data Source=LAPTOP-R6OUBGEG;Initial Catalog=cinethequeDB;User ID=marvin;Password=Soleil.123";
        //    string connectionString = @"Data Source=cinesrv.database.windows.net;Initial Catalog=cinethequeBDD;User ID=test;Password=cine1234!;Encrypt=True;TrustServerCertificate=True;";

        //    string sql = @"SELECT a.* FROM locations l INNER JOIN articles a ON l.article_id = a.id WHERE l.utilisateur_id = @id";

        //    SqlConnection connection = new SqlConnection(connectionString);
        //    connection.Open();

        //    SqlCommand command = new SqlCommand(sql, connection);

        //    command.Parameters.AddWithValue("@id", id);
        //    SqlDataReader reader = command.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        Articles article = new Articles
        //        {
        //            getId = (int)reader["id"],
        //            getPrice = (double)reader["prix"],
        //            getName = reader["nom"].ToString(),
        //            getCatId = (int)reader["categorie_id"],
        //            getDescription = reader["description"].ToString(),
        //            getTotalQty = (int)reader["qte_totale"],
        //            getDispoQty = (int)reader["qte_dispo"]
        //        };
        //        articles.Add(article);
        //    }
        //    connection.Close();

        //    return articles;
        //}
    }
}