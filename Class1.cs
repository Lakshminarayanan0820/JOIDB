using System;
using System.Data.Sqlite;

public class DatabaseConnection
{
    private SQLiteConnection _connection;

    public void InitializeDatabase()
    {
        // Establish SQLite connection  
        _connection = new SQLiteConnection("Data Source=joi_energy.db;Version=3;");
        _connection.Open();

        // Create table if it doesn't exist  
        string createTableQuery = @"
           CREATE TABLE IF NOT EXISTS EnergyData (
               Id INTEGER PRIMARY KEY AUTOINCREMENT,
               MeterId TEXT NOT NULL,
               EnergyUsage REAL NOT NULL,
               Timestamp DATETIME DEFAULT CURRENT_TIMESTAMP
           );
       ";

        using (var command = new SQLiteCommand(createTableQuery, _connection))
        {
            command.ExecuteNonQuery();
        }
    }

    public void CloseConnection()
    {
        _connection?.Close();
    }
}
