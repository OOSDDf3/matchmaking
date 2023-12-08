using Azure;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;

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
            dbCon.Password = "MyNewPass";
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
                    string query = "INSERT INTO `account` (`user_ID`, `name`, `email`, `password`, `birthdate`, `address`, `gender`, `language`) VALUES (NULL, @na, @em, @pa, @ag, @ad, @ge, @la);";
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


        public void UpdateAccount(int userID, string name, string email, string password, string address, string gender, string language)
        {
            try
            {
                if (dbCon.IsConnect())
                {
                    string query = "UPDATE `account` SET `name` = @na, `email` = @em, `password` = @pa,`address` = @ad, `gender` = @ge, `language` = @la WHERE `user_ID` = @userID;";
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    cmd.Parameters.Add("@userID", MySqlDbType.Int32, 4).Value = userID;
                    cmd.Parameters.Add("@na", MySqlDbType.VarChar, 100).Value = name;
                    cmd.Parameters.Add("@em", MySqlDbType.VarChar, 100).Value = email;
                    cmd.Parameters.Add("@pa", MySqlDbType.VarChar, 100).Value = password;
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

        public bool CheckLogin(string email, string password, out int user_ID)
        {
            user_ID = Int32.MinValue;
            try
            {
                if (dbCon.IsConnect())
                {
                    string queryForID = "SELECT user_ID FROM Account WHERE email = @email AND password = @password";
                    var cmdID = new MySqlCommand(queryForID, dbCon.Connection);
                    cmdID.Parameters.AddWithValue("@email", email);
                    cmdID.Parameters.AddWithValue("@password", password);
                    var user_ID_reader = cmdID.ExecuteReader();
                    while (user_ID_reader.Read())
                    {
                        user_ID = Int32.Parse(user_ID_reader.GetValue(0).ToString());
                    }
                    user_ID_reader.Close();

                    string queryForInfo = "SELECT Count(*) FROM Account WHERE email = @email AND password = @password";
                    var cmd = new MySqlCommand(queryForInfo, dbCon.Connection);
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
                dbCon.Close();
                return false;
            }
        }

        public Dictionary<string, string> ShowUserInformation(int user_ID, string query)
        {
            Dictionary<string, string> keyValuePairs = new();
            try
            {
                if (dbCon.IsConnect())
                {
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    cmd.Parameters.AddWithValue("@user_ID", user_ID);
                    var reader = cmd.ExecuteReader();
                    string[] keys = new string[8] { "user_ID", "name", "email", "password", "birthdate", "address", "gender", "language"};

                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write(reader.GetValue(i).ToString() + " ");
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
                dbCon.Close();
                return keyValuePairs;
            }
        }


        public int getUserID(string email, string password)
        {
            int user_ID = Int32.MinValue;
            try
            {
                if (dbCon.IsConnect())
                {
                    string queryForID = "SELECT user_ID FROM Account WHERE email = @email AND password = @password";
                    var cmdID = new MySqlCommand(queryForID, dbCon.Connection);
                    cmdID.Parameters.AddWithValue("@email", email);
                    cmdID.Parameters.AddWithValue("@password", password);
                    var user_ID_reader = cmdID.ExecuteReader();
                    while (user_ID_reader.Read())
                    {
                        user_ID = Int32.Parse(user_ID_reader.GetValue(0).ToString());
                    }
                    user_ID_reader.Close();
                }
                return user_ID;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                dbCon.Close();
                return user_ID;
            }
        }

        public List<string> GetInterestCategories()
        {
            List<string> categories = new();
            try
            {
                if (dbCon.IsConnect())
                {
                    string query = "SELECT DISTINCT category FROM Interests";
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.WriteLine(reader.GetValue(i).ToString());
                            categories.Add(reader.GetValue(i).ToString());
                        }
                    }
                    reader.Close();
                }
                return categories;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                dbCon.Close();
                return categories;
            }
        }

        public List<string> GetInterestsWithCategory(string category)
        {
            List<string> interests = new();
            try
            {
                if (dbCon.IsConnect())
                {
                    string query = "SELECT name FROM Interests WHERE category = @ca";
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    cmd.Parameters.AddWithValue("@ca", category);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.WriteLine(reader.GetValue(i).ToString());
                            interests.Add(reader.GetValue(i).ToString());
                        }
                    }
                    reader.Close();
                }
                return interests;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                dbCon.Close();
                return interests;
            }
        }

        public void InsertIntoUserInterestList(int user_ID, List<string> interests)
        {
            try
            {
                if (dbCon.IsConnect())
                {
                    foreach (string interest in interests)
                    {
                        string querySelect = "SELECT  interest_Id FROM Interests WHERE name = @in";
                        var cmdSelect = new MySqlCommand(querySelect, dbCon.Connection);
                        cmdSelect.Parameters.AddWithValue("@in", interest);
                        var reader = cmdSelect.ExecuteReader();
                        int interest_ID = Int32.MinValue;
                        while (reader.Read())
                        {
                            interest_ID = Int32.Parse(reader.GetValue(0).ToString());
                        }
                        reader.Close();
                        Debug.WriteLine(interest_ID);
                        Debug.WriteLine($"{user_ID}, {interest_ID}");

                        string queryInsert = "INSERT INTO `userinterestlist` (`user_ID`, `interest_ID`) VALUES (@us, @inID)";
                        var cmdInsert = new MySqlCommand(queryInsert, dbCon.Connection);
                        cmdInsert.Parameters.AddWithValue("@us", user_ID);
                        cmdInsert.Parameters.AddWithValue("@inID", interest_ID);
                        cmdInsert.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                dbCon.Close();
            }
        }

        public List<string> GetInterestsWithUserID(int user_ID)
        {
            List<string> interests = new();
            try
            {
                if (dbCon.IsConnect())
                {
                    string query = "SELECT name FROM Interests WHERE interest_ID IN (SELECT interest_ID FROM userinterestlist WHERE user_ID = @us)";
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    cmd.Parameters.AddWithValue("@us", user_ID);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.WriteLine(reader.GetValue(i).ToString());
                            interests.Add(reader.GetValue(i).ToString());
                        }
                    }
                    reader.Close();
                }
                return interests;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                dbCon.Close();
                return interests;
            }
        }
        public void InsertIntoUserInterestList(int user_ID, List<string> interests, Byte[]picture)
        {
            try
            {
                if (dbCon.IsConnect())
                {
                    foreach (string interest in interests)
                    {
                        string querySelect = "SELECT  interest_Id FROM Interests WHERE name = @in";
                        var cmdSelect = new MySqlCommand(querySelect, dbCon.Connection);
                        cmdSelect.Parameters.AddWithValue("@in", interest);
                        var reader = cmdSelect.ExecuteReader();
                        int interest_ID = Int32.MinValue;
                        while (reader.Read())
                        {
                            interest_ID = Int32.Parse(reader.GetValue(0).ToString());
                        }
                        reader.Close();
                        Debug.WriteLine(interest_ID);
                        Debug.WriteLine($"{user_ID}, {interest_ID}");

                        string queryInsertInterest = "INSERT INTO `userinterestlist` (`user_ID`, `interest_ID`) VALUES (@us, @inID)";
                        var cmdInsertInterest = new MySqlCommand(queryInsertInterest, dbCon.Connection);
                        cmdInsertInterest.Parameters.AddWithValue("@us", user_ID);
                        cmdInsertInterest.Parameters.AddWithValue("@inID", interest_ID);
                        cmdInsertInterest.ExecuteNonQuery();
                    }

                    string queryInsertPicture = "INSERT INTO `profilepicture` (`user_ID`, `picture`) VALUES (@us1, @pi)";
                    var cmdInsertPicture = new MySqlCommand(queryInsertPicture, dbCon.Connection);
                    cmdInsertPicture.Parameters.Add("@us1", MySqlDbType.VarChar, 100).Value = user_ID;
                    cmdInsertPicture.Parameters.Add("@pi", MySqlDbType.Blob).Value = picture;
                    cmdInsertPicture.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                dbCon.Close();
            }
        }

    }
}
