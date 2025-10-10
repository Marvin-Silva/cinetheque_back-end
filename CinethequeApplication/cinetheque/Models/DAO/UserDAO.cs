using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace cinetheque.Models
{
    public class UserDAO
    {

        public UserInfo SelectUserInfo(int id)
        {
            UserInfo user = new UserInfo();
            string connectionString = @"Data Source=LAPTOP-R6OUBGEG;Initial Catalog=cinethequeDB;User ID=marvin;Password=Soleil.123";

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string sql = "SELECT * FROM utilisateur_infos WHERE utilisateur_id=@id";;
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int idInfo = Convert.ToInt32(reader.GetValue(0));
                string nom = reader.GetString(1);
                string prenom = reader.GetString(2);
                string adresse = reader.GetString(3);
                int utilisateur_id = Convert.ToInt32(reader.GetValue(4));

                user = new UserInfo(idInfo, nom, prenom, adresse, utilisateur_id);
                return user;
            }

            reader.Close();
            connection.Close();
            return user;
        }


        public bool SelectUser(User user)
        {
            string connectionString = @"Data Source=cinesrv.database.windows.net;Initial Catalog=cinethequeBDD;User ID=test;Password=cine1234!;Encrypt=True;TrustServerCertificate=True;";

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string sql = "SELECT id, login, mdp FROM utilisateurs WHERE login = @login AND mdp = @mdp";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@login", user.getLogin);
            command.Parameters.AddWithValue("@mdp", user.getPwd);

            SqlDataReader reader = command.ExecuteReader();

            bool userFound = false;

            while (reader.Read())
            {
                int dbId = reader.GetInt32(0);
                string dbLogin = reader.GetString(1);
                string dbPwd = reader.GetString(2);

                if (dbLogin.Equals(user.getLogin) && dbPwd.Equals(user.getPwd))
                {
                    user.Id =(int) reader["id"];
                    user.getLogin =(string) reader["login"];
                    user.getPwd = (string) reader["mdp"];
                    
                    userFound = true;
                }
            }

            reader.Close();
            connection.Close();

            if (userFound)
            {
                Console.WriteLine("Utilisateur trouvé !");
            }
            else
            {
                Console.WriteLine("Login ou mot de passe incorrect.");
            }
            return userFound;
        }

        public void InsertUser(UserDto user)
        {
            if (user == null)
            {
                Console.WriteLine("L'objet en paramètre est NULL");
                return;
            }

            string connectionString = @"Data Source=cinesrv.database.windows.net;Initial Catalog=cinethequeBDD;User ID=test;Password=cine1234!;Encrypt=True;TrustServerCertificate=True;";

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

                string insertAuth = "INSERT INTO utilisateurs (login, mdp, role) VALUES(@login, @mdp, @roles)";
                using (SqlCommand insertCmd = new SqlCommand(insertAuth, connection))
                {
                    //string hashedPwd = BCrypt.Net.BCrypt.HashPassword(user.getPwd);

                    insertCmd.Parameters.AddWithValue("@login", user.getLogin);
                    insertCmd.Parameters.AddWithValue("@mdp", user.getPwd);
                    insertCmd.Parameters.AddWithValue("@roles", user.getRole ?? "utilisateur");

                    insertCmd.ExecuteNonQuery();
                    Console.WriteLine("Utilisateur ajouté avec succès !");
                }

                // Si on arrive ici, le login n’existe pas → on crée le nouvel utilisateur
                string insertSql = "INSERT INTO utilisateur_infos (nom, prenom, adresse, utilisateur_id) VALUES (@nom, @prenom,@adresse, @userId)";

                using (SqlCommand insertCmd = new SqlCommand(insertSql, connection))
                {
                    insertCmd.Parameters.AddWithValue("@nom", user.getUserName);
                    insertCmd.Parameters.AddWithValue("@prenom", user.getUserFirstName);
                    insertCmd.Parameters.AddWithValue("@adresse", user.getUserAdress);
                    insertCmd.Parameters.AddWithValue("@userId", user.getUserId);

                    insertCmd.ExecuteNonQuery();
                    Console.WriteLine("Utilisateur info ajouté avec succès !");
                }
            }
        }

    }
}