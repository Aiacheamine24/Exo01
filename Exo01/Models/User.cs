using System;
using System.Data.SqlClient;
using System.Web.WebPages;

namespace Exo01.Models
{
    public class User
    {
        private string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EXO01;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        private SqlConnection Connection { get; set; }

        // Properties
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        // Constructors
        public User() { }

        public User(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        // Methods
        // Login
        public bool Login()
        {
            // Verification des champs
            if (this.Username.IsEmpty() || this.Password.IsEmpty())
            {
                return false;
            }

            // Recuperation d'un objet de connexion
            using (SqlConnection connection = GetConnection())
            {
                // Ouverture de la connexion
                connection.Open();
                // Creation de la requete
                string query = "SELECT * FROM [User] WHERE Username = @username AND Password = @password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", this.Username);
                    command.Parameters.AddWithValue("@password", this.Password);

                    // Execution de la requete
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Verification du resultat
                        if (reader.Read())
                        {
                            this.Id = (int)reader["Id"];
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
        }

        private SqlConnection GetConnection()
        {
            if (this.Connection == null)
            {
                this.Connection = new SqlConnection(this.ConnectionString);
            }
            return this.Connection;
        }
    }
}
