using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;


namespace Memory_WPF_OOP.Controllers
{
    public class User
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int Score { get; set; }
        public DateTime RegistrationDate { get; set; }
    }

    public class DatabaseService
    {
        private readonly string connectionString;

        public DatabaseService()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = Path.Combine(baseDirectory, "CartUserBaseDeDonnees.db");
            connectionString = $"Data Source={dbPath}";

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Utilisateurs (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Nom TEXT NOT NULL,
                            Score INTEGER NOT NULL,
                            RegistrationDate TEXT NOT NULL
                        );";
                    command.ExecuteNonQuery();
                }
            }
        }

        public void InsertUser(string userName, int score = 0)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        INSERT INTO Utilisateurs (Nom, Score, RegistrationDate)
                        VALUES ($nom, $score, $date);";

                    command.Parameters.AddWithValue("$nom", userName);
                    command.Parameters.AddWithValue("$score", score);
                    command.Parameters.AddWithValue("$date", DateTime.Now.ToString("s"));
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<User> GetUsers()
        {
            var users = new List<User>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Id, Nom, Score, RegistrationDate FROM Utilisateurs;";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User
                            {
                                Id = reader.GetInt32(0),
                                Nom = reader.GetString(1),
                                Score = reader.GetInt32(2),
                                RegistrationDate = DateTime.Parse(reader.GetString(3))
                            });
                        }
                    }
                }
            }
            return users;
        }
    }
}
