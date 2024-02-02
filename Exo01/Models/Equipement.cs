using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Exo01.Models
{
    public class Equipement
    {
        // Properties
        private static string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EXO01;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        private static SqlConnection Connection { get; set; }
        public int SN { get; set; }
        public string Nom { get; set; }
        public string Type { get; set; }
        public decimal Prix { get; set; }
        public string Description { get; set; }

        // Constructors
        public Equipement() { }
        public Equipement(int sn, string nom, string type, decimal prix, string description)
        {
            this.SN = sn;
            this.Nom = nom;
            this.Type = type;
            this.Prix = prix;
            this.Description = description;
        }

        // Methods
        // GetEquipements
        public static List<Equipement> getEquipments()
        {
            List<Equipement> equipments = new List<Equipement>();
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM [Equipement]";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Equipement equipment = new Equipement();
                            equipment.SN = (int)reader["SN"];
                            equipment.Nom = (string)reader["Nom"];
                            equipment.Type = (string)reader["Type"];
                            equipment.Prix = (decimal)reader["Prix"];
                            equipment.Description = (string)reader["Description"];
                            equipments.Add(equipment);
                        }
                    }
                }
                connection.Close();
            }
            return equipments;
        }
        // GetConnection
        private static SqlConnection GetConnection()
        {
            return Connection = new SqlConnection(ConnectionString);
        }

        public static bool addEquipment(Equipement equipement)
        {
            if (equipement == null || string.IsNullOrEmpty(equipement.Nom) || string.IsNullOrEmpty(equipement.Type) || string.IsNullOrEmpty(equipement.Description))
            {
                return false;
            }
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string query = "INSERT INTO [Equipement] (Nom, Type, Prix, Description) VALUES ( @Nom, @Type, @Prix, @Description)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nom", equipement.Nom);
                    command.Parameters.AddWithValue("@Type", equipement.Type);
                    command.Parameters.AddWithValue("@Prix", equipement.Prix);
                    command.Parameters.AddWithValue("@Description", equipement.Description);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return true;
        }
        public static bool deleteEquipment(int sn)
        {
            if (sn <= 0)
            {
                return false;
            }
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string query = "DELETE FROM [Equipement] WHERE SN = @SN";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SN", sn);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return true;
        }
        // ModifyEquipment
        public static bool modifyEquipment(Equipement equipement)
        {
            if (equipement == null || string.IsNullOrEmpty(equipement.Nom) || string.IsNullOrEmpty(equipement.Type) || string.IsNullOrEmpty(equipement.Description))
            {
                return false;
            }
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string query = "UPDATE [Equipement] SET Nom = @Nom, Type = @Type, Prix = @Prix, Description = @Description WHERE SN = @SN";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SN", equipement.SN);
                    command.Parameters.AddWithValue("@Nom", equipement.Nom);
                    command.Parameters.AddWithValue("@Type", equipement.Type);
                    command.Parameters.AddWithValue("@Prix", equipement.Prix);
                    command.Parameters.AddWithValue("@Description", equipement.Description);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return true;
        }
        // GetEquipment with SN
        public static Equipement getEquipment(int sn)
        {
            Equipement equipment = new Equipement();
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM [Equipement] WHERE SN = @SN";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SN", sn);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            equipment.SN = (int)reader["SN"];
                            equipment.Nom = (string)reader["Nom"];
                            equipment.Type = (string)reader["Type"];
                            equipment.Prix = (decimal)reader["Prix"];
                            equipment.Description = (string)reader["Description"];
                        }
                    }
                }
                connection.Close();
            }
            return equipment;
        }
    }
}