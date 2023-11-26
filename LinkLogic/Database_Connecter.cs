using Azure;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinkApplication
{
    public class Database_Connecter
    {
        public DatabaseConnection dbCon = null;

        public Database_Connecter()
        {
            ConnectToLocalDatabase();
            //InsertAccount("Joas", "joas@hotmail.com", "securepassword", 70, "streetname 42", "Male", "English");
            //CheckLogin("Joas Weeda", "newpassword");
            //CheckLogin("Joas", "securepassword");
            //CheckLogin("Joas", "unsecurepassword");
            //ShowUserInformation("Joas Weeda", "newpassword", "SELECT * FROM Account WHERE name = @userName AND password = @password");
            //ShowUserInformation("Joas Weeda", "falsenewpassword", "SELECT * FROM Account WHERE name = @userName AND password = @password");
            //dbCon.Close();
        }

        protected void ConnectToLocalDatabase()
        {
            dbCon = DatabaseConnection.Instance();
            dbCon.Server = "localhost";
            dbCon.Database = "matchmaking";
            dbCon.UserName = "root";
            dbCon.Password = "";
        }

        protected void ConnectToServerDatabase()
        {
            dbCon = DatabaseConnection.Instance();
            dbCon.Server = "localhost";
            dbCon.Database = "matchmaking";
            dbCon.UserName = "SA";
            dbCon.Password = "@Matchingf3";
        }

        public void InsertAccount(string name, string email, string password, int age, string address, string gender, string language)
        {
            try
            {
                if (dbCon.IsConnect())
                {
                    string query = "INSERT INTO `account` (`user_ID`, `name`, `email`, `password`, `age`, `address`, `gender`, `language`) VALUES (NULL, @na, @em, @pa, @ag, @ad, @ge, @la);";
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    cmd.Parameters.Add("@na", MySqlDbType.VarChar, 100).Value = name;
                    cmd.Parameters.Add("@em", MySqlDbType.VarChar, 100).Value = email;
                    cmd.Parameters.Add("@pa", MySqlDbType.VarChar, 100).Value = password;
                    cmd.Parameters.Add("@ag", MySqlDbType.Int32, 4).Value = age;
                    cmd.Parameters.Add("@ad", MySqlDbType.VarChar, 100).Value = address;
                    cmd.Parameters.Add("@ge", MySqlDbType.VarChar, 10).Value = gender;
                    cmd.Parameters.Add("@la", MySqlDbType.VarChar, 50).Value = language;
                    Console.WriteLine(cmd.ExecuteNonQuery());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public bool CheckLogin(string email, string password)
        {
            try
            {
                if (dbCon.IsConnect())
                {
                    string query = "SELECT Count(*) FROM Account WHERE email = @email AND password = @password";
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);
                    var count = cmd.ExecuteScalar();
                    if (count.ToString().Equals("1"))
                    {
                        Console.WriteLine("Login succesfull");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Login unsuccesfull");
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public Dictionary<string, string> ShowUserInformation(string email, string password, string query)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            try
            {
                if (dbCon.IsConnect() & CheckLogin(email, password))
                {
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);
                    var reader = cmd.ExecuteReader();
                    string[] keys = new string[8] {"user_ID", "name", "email", "password", "age", "address", "gender", "language" };

                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            keyValuePairs.Add(keys[i], reader.GetValue(i).ToString());
                        }
                        Console.WriteLine();
                    }
                    reader.Close();

                    return keyValuePairs;
                }
                return keyValuePairs;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return keyValuePairs;
            }
        }
    }
}
