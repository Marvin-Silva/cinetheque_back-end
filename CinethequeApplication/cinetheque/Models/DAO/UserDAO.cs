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
            string connectionString = @"Data Source=cinesrv.database.windows.net;Initial Catalog=cinethequeBDD;User ID=test;Password=cine1234!;Encrypt=True;TrustServerCertificate=True;";

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
            //string connectionString = @"Data Source=LAPTOP-R6OUBGEG;Initial Catalog=cinethequeDB;User ID=marvin;Password=Soleil.123";

            SqlConnection connection = new SqlConnection(connectionString);
            
                connection.Open();

                string sql = "SELECT login, mdp FROM utilisateurs WHERE login=@userDtoLogin AND mdp=@userDtoMdp"; ;

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("userDtoLogin", user.getLogin);
                command.Parameters.AddWithValue("userDtoMdp", user.getPwd);

                SqlDataReader reader = command.ExecuteReader();
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

                string insertUser = @"INSERT INTO utilisateurs (login, mdp, role) VALUES (@login, @mdp, @roles); SELECT SCOPE_IDENTITY();"; // récupère l'ID généré

                string insertInfos = @"INSERT INTO utilisateur_infos (nom, prenom, adresse, utilisateur_id) VALUES (@nom, @prenom, @adresse, @utilisateur_id)";

                //string hashedPwd = BCrypt.Net.BCrypt.HashPassword(user.getPwd);

                // 1. Insertion dans utilisateurs
                SqlCommand cmdUser = new SqlCommand(insertUser, connection);
                cmdUser.Parameters.AddWithValue("@login", user.getLogin);
                cmdUser.Parameters.AddWithValue("@mdp", user.getPwd);
                cmdUser.Parameters.AddWithValue("@roles", user.getRole ?? "utilisateur");
                int utilisateurId = Convert.ToInt32(cmdUser.ExecuteScalar());

                // 2. Insertion dans utilisateur_infos
                SqlCommand cmdInfos = new SqlCommand(insertInfos, connection);
                cmdInfos.Parameters.AddWithValue("@nom", user.getUserName);
                cmdInfos.Parameters.AddWithValue("@prenom", user.getUserFirstName);
                cmdInfos.Parameters.AddWithValue("@adresse", user.getUserAdress);
                cmdInfos.Parameters.AddWithValue("@utilisateur_id", utilisateurId);
                cmdInfos.ExecuteNonQuery();

                connection.Close();
            }
        }

    }
}