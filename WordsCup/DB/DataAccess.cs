using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.IO;
using System.Windows;
using System.Data;

namespace WordsCup.DB
{
   public static class DataAccess
    {
        private static readonly string dbpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DB\\WordsCup.db");


        //public async static Task InitializeDatabase()
        //{
        //    using (var db = new SqliteConnection($"Filename={dbpath}"))
        //    {
        //        db.Open();

        //        string tableCommand = "CREATE TABLE IF NOT " +
        //            "EXISTS MyTable (Primary_Key INTEGER PRIMARY KEY, " +
        //            "Text_Entry NVARCHAR(2048) NULL)";

        //        var createTable = new SqliteCommand(tableCommand, db);

        //        createTable.ExecuteReader();
        //        db.Close();
        //    }
        //}

        public static void AddUser(string username, string password)
        {
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

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
                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT COUNT(*) FROM Users WHERE login = @Username AND password = @Password", db);
                selectCommand.Parameters.AddWithValue("@Username", username);
                selectCommand.Parameters.AddWithValue("@Password", password);

                int count = Convert.ToInt32(selectCommand.ExecuteScalar());
                db.Close();

                return count == 1;
            }
        }

        public static void UpdateUser(User user)
        {
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand updateCommand = new SqliteCommand
                    ("UPDATE Users SET balance = @Balance, saveWord = @SaveWord, difficulty = @Difficulty WHERE login = @Login", db);
                updateCommand.Parameters.AddWithValue("@Login", user.login);
                updateCommand.Parameters.AddWithValue("@Balance", user.balance);

                SqliteParameter saveWordParameter = new SqliteParameter("@SaveWord", DbType.String);
                saveWordParameter.Value = (object)user.saveWord ?? DBNull.Value;
                updateCommand.Parameters.Add(saveWordParameter);

                updateCommand.Parameters.AddWithValue("@Difficulty", user.successPoint);
                updateCommand.ExecuteNonQuery();

                db.Close();
            }
        }

        public static User GetUser(string username)
        {
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * FROM Users WHERE login = @Username", db);
                selectCommand.Parameters.AddWithValue("@Username", username);

                using (var reader = selectCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        User user = new User
                        {
                            id = reader.GetInt32(0),
                            login = reader.GetString(1),
                            password = reader.GetString(2),
                            balance = reader.GetInt32(3),
                            saveWord = reader.IsDBNull(4) ? null : reader.GetString(4),
                            successPoint = reader.IsDBNull(5) ? 0 : reader.GetInt32(5),
                        };
                        db.Close();

                        return user;
                    }
                }
                db.Close();
            }
            return null;
        }

    }
}
