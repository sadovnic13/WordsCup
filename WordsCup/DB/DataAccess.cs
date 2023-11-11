using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace WordsCup.DB
{
   public static class DataAccess
    {
        private static readonly string dbpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DB\\WordsCup.db");


        public async static Task InitializeDatabase()
        {
            string dbpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WordsCup.db");

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                string tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS MyTable (Primary_Key INTEGER PRIMARY KEY, " +
                    "Text_Entry NVARCHAR(2048) NULL)";

                var createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
                db.Close();
            }
        }

        public static void AddUser(string username, string password)
        {
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO Users (login, password) VALUES (@Username, @Password);";
                insertCommand.Parameters.AddWithValue("@Username", username);
                insertCommand.Parameters.AddWithValue("@Password", password);
                insertCommand.ExecuteReader();

                db.Close();
            }
        }

        public static bool UserExists(string username, string password)
        {
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var selectCommand = new SqliteCommand
                    ("SELECT COUNT(*) FROM Users WHERE login = @Username AND password = @Password", db);
                selectCommand.Parameters.AddWithValue("@Username", username);
                selectCommand.Parameters.AddWithValue("@Password", password);

                int count = Convert.ToInt32(selectCommand.ExecuteScalar());
                db.Close();

                return count == 1;
            }
        }



    }
}
