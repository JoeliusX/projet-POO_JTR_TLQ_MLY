using System;
using System.IO;
using Microsoft.Data.Sqlite;

namespace Memory_WPF_OOP
{
    public class Board
    {
        public void DisplayDatabaseContents()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string dbPath = Path.Combine(baseDirectory, "CartUserBaseDeDonnees.db");
            string connectionString = $"Data Source={dbPath}";

            using (var connection = new SqliteConnection(connectionString))
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
                        )";
                    command.ExecuteNonQuery();
                }

                using (var selectCmd = connection.CreateCommand())
                {
                    selectCmd.CommandText = "SELECT Id, Nom, Score, RegistrationDate FROM Utilisateurs";
                    using (var reader = selectCmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("Table Utilisateurs out of date.");
                        }
                        else
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string nom = reader.GetString(1);
                                int score = reader.GetInt32(2);
                                string registrationDate = reader.GetString(3);

                                Console.WriteLine($"Id: {id}, Nom: {nom}, Score: {score}, RegistrationDate: {registrationDate}");
                            }
                        }
                    }
                }
            }
        }
    }
}
