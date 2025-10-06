using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace cinetheque.Models
{
    public class UserDAO
    {
        public void InsertUser(User user)
        {
            string connectionString = @"Data Source=LAPTOP-R6OUBGEG;
                                Initial Catalog=cinethequeDB;
                                User ID=Marvin;
                                Password=Soleil.123;
                                Encrypt=True;
                                TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT login, mdp FROM utilisateurs";

                using (SqlCommand command = new SqlCommand(sql, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    bool loginExiste = false;

                    while (reader.Read())
                    {
                        string loginDb = reader.GetString(0);
                        string pwdDb = reader.GetString(1);

                        if (user.getLogin.Equals(loginDb) && user.getPwd.Equals(pwdDb))
                        {
                            loginExiste = true;
                            break;
                        }
                    }

                    reader.Close();

                    if (loginExiste)
                    {
                        Console.WriteLine("Ce login existe déjà, création refusée !");
                        return;
                    }
                }

                // Si on arrive ici, le login n’existe pas → on crée le nouvel utilisateur
                string insertSql = "INSERT INTO utilisateurs (login, mdp, role) VALUES (@login, @mdp, @role)";

                using (SqlCommand insertCmd = new SqlCommand(insertSql, connection))
                {
                    insertCmd.Parameters.AddWithValue("@login", user.getLogin);
                    insertCmd.Parameters.AddWithValue("@mdp", user.getPwd);
                    insertCmd.Parameters.AddWithValue("@role", user.getRole ?? "utilisateur");
                    
                    insertCmd.ExecuteNonQuery();
                    Console.WriteLine("Utilisateur ajouté avec succès !");
                }
            }
        }

    }
}