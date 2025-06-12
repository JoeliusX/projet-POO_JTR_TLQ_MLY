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
        public User GetUserByName(string name)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "SELECT Id, Nom, Score, RegistrationDate FROM Utilisateurs WHERE Nom = $nom;";
                    cmd.Parameters.AddWithValue("$nom", name);

                    using (var r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            return new User
                            {
                                Id = r.GetInt32(0),
                                Nom = r.GetString(1),
                                Score = r.GetInt32(2),
                                RegistrationDate = DateTime.Parse(r.GetString(3))
                            };
                        }
                    }
                }
            }
            return null;
        }

        public User CreateUser(string name, int score = 0)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        @"INSERT INTO Utilisateurs (Nom, Score, RegistrationDate)
                          VALUES ($nom,$score,$date);
                          SELECT last_insert_rowid();";
                    cmd.Parameters.AddWithValue("$nom", name);
                    cmd.Parameters.AddWithValue("$score", score);
                    cmd.Parameters.AddWithValue("$date", DateTime.Now.ToString("s"));

                    long id = (long)cmd.ExecuteScalar();

                    return new User
                    {
                        Id = (int)id,
                        Nom = name,
                        Score = score,
                        RegistrationDate = DateTime.Now
                    };
                }
            }
        }
        public void UpdateScore(int userId, int newScore)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "UPDATE Utilisateurs SET Score = $score WHERE Id = $id;";
                    cmd.Parameters.AddWithValue("$score", newScore);
                    cmd.Parameters.AddWithValue("$id", userId);
                    cmd.ExecuteNonQuery();
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
        public List<User> GetTopUsers(int limit = 10)
        {
            var list = new List<User>();

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT Id, Nom, Score, RegistrationDate
                FROM Utilisateurs
                ORDER BY Score DESC, Nom ASC
                LIMIT $lim;";
                    cmd.Parameters.AddWithValue("$lim", limit);

                    using (var r = cmd.ExecuteReader())
                        while (r.Read())
                            list.Add(new User
                            {
                                Id = r.GetInt32(0),
                                Nom = r.GetString(1),
                                Score = r.GetInt32(2),
                                RegistrationDate = DateTime.Parse(r.GetString(3))
                            });
                }
            }
            return list;
        }
    }
}
