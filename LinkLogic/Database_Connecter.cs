using Azure;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

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

        //Methode voor nieuwe account
        public void InsertAccount(string name, string email, string password, int birthdate, string address, string gender, string language)
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
                    cmd.Parameters.Add("@ag", MySqlDbType.Int32, 4).Value = birthdate;
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


        //Methode voor bijwerken van een bestaand account
        public void UpdateAccount(int userID, string name, string email, string address, string gender, string language)
        {
            try
            {
                if (dbCon.IsConnect())
                {
                    string query = "UPDATE `account` SET `name` = @na, `email` = @em, `address` = @ad, `gender` = @ge, `language` = @la WHERE `user_ID` = @userID;";
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    cmd.Parameters.Add("@userID", MySqlDbType.Int32, 4).Value = userID;
                    cmd.Parameters.Add("@na", MySqlDbType.VarChar, 100).Value = name;
                    cmd.Parameters.Add("@em", MySqlDbType.VarChar, 100).Value = email;
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

        public void UpdatePassword(int userID, string password)
        {
            try
            {
                if (dbCon.IsConnect())
                {
                    string query = "UPDATE `account` SET `password` = @pa WHERE `user_ID` = @userID;";
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    cmd.Parameters.Add("@userID", MySqlDbType.Int32, 4).Value = userID;
                    cmd.Parameters.Add("@pa", MySqlDbType.VarChar, 100).Value = password;

                    Console.WriteLine(cmd.ExecuteNonQuery());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
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
                return false;
            }
        }

        public bool CheckLogin(string email, string password)
        {
            try
            {
                if (dbCon.IsConnect())
                {
                    string queryForID = "SELECT user_ID FROM Account WHERE email = @email AND password = @password";
                    var cmdID = new MySqlCommand(queryForID, dbCon.Connection);
                    cmdID.Parameters.AddWithValue("@email", email);
                    cmdID.Parameters.AddWithValue("@password", password);

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
                return false;
            }
        }


        public Dictionary<string, string> ShowUserInformation(int user_ID)
        {
            Dictionary<string, string> keyValuePairs = new();
            string query = "SELECT * FROM Account WHERE user_ID = @user_ID";
            try
            {
                if (dbCon.IsConnect())
                {
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    cmd.Parameters.AddWithValue("@user_ID", user_ID);
                    var reader = cmd.ExecuteReader();
                    string[] keys = new string[8] { "user_ID", "name", "email", "password", "birthdate", "address", "gender", "language" };

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
                return keyValuePairs;
            }
        }

        public List<string> ShowUserInterests(int user_ID)
        {
            List<string> Interest_ID = new();
            List<string> Interest_name = new();

            string query = "SELECT interest_ID FROM userinterestlist WHERE user_ID = @user_ID";
            try
            {
                if (dbCon.IsConnect())
                {
                    var cmdInterestID = new MySqlCommand(query, dbCon.Connection);
                    cmdInterestID.Parameters.AddWithValue("@user_ID", user_ID);
                    var reader = cmdInterestID.ExecuteReader();

                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Debug.Write(reader.GetValue(i).ToString() + " ");
                            Interest_ID.Add(reader.GetValue(i).ToString());
                        }
                    }
                    reader.Close();

                    var queryName = "SELECT name FROM interests WHERE interest_ID = @interest_ID";
                    var cmdInterestName = new MySqlCommand(queryName, dbCon.Connection);

                    foreach (var id in Interest_ID)
                    {
                        cmdInterestName.Parameters.Clear();
                        cmdInterestName.Parameters.AddWithValue("@interest_ID", id);

                        using var readerInterestName = cmdInterestName.ExecuteReader();

                        while (readerInterestName.Read())
                        {
                            Interest_name.Add(readerInterestName["name"].ToString());
                            Debug.Write(readerInterestName["name"].ToString() + " ");
                        }
                    }
                    return Interest_name;
                }
                return Interest_name;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Interest_name;
            }
        }

        //code voor het halen van user id's uit de database met matchende interesses
        public Dictionary<int, int> GetMatchingUser(int user_ID)
        {
            //code voor ophalen van intereses van desbetreffende gebruiker
            List<string> Interest_ID = new();
            string queryInterest_ID = "SELECT interest_ID FROM userinterestlist WHERE user_ID = @user_ID";

            var cmdInterestID = new MySqlCommand(queryInterest_ID, dbCon.Connection);
            cmdInterestID.Parameters.AddWithValue("@user_ID", user_ID);
            var reader = cmdInterestID.ExecuteReader();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {

                    Interest_ID.Add(reader.GetValue(i).ToString());
                }
            }
            reader.Close();

            //code voor ophalen users die al geliked of disliked zijn
            List<int> likedUser_ID = new();
            string queryLikeduser_ID = "SELECT user_ID_Liked FROM userlikesdislikes WHERE user_ID = @user_ID";

            var cmdLikeduser_ID = new MySqlCommand(queryLikeduser_ID, dbCon.Connection);
            cmdLikeduser_ID.Parameters.AddWithValue("@user_ID", user_ID);
            var readerLikeduser_ID = cmdLikeduser_ID.ExecuteReader();

            while (readerLikeduser_ID.Read())
            {
                for (int i = 0; i < readerLikeduser_ID.FieldCount; i++)
                {

                    likedUser_ID.Add(Int32.Parse(readerLikeduser_ID.GetValue(i).ToString()));
                }
            }
            readerLikeduser_ID.Close();

            //code voor ophalen andere users met matchende interresses
            Dictionary<int, int> users = new();

            string queryUser_ID = "SELECT user_ID FROM userinterestlist WHERE interest_ID = @interest_ID";
            var cmdSelect = new MySqlCommand(queryUser_ID, dbCon.Connection);

            foreach (var id in Interest_ID)
            {
                cmdSelect.Parameters.Clear();
                cmdSelect.Parameters.AddWithValue("@interest_ID", id);
                cmdSelect.Parameters.AddWithValue("@user_ID", user_ID);

                var readerID = cmdSelect.ExecuteReader();

                while (readerID.Read())
                {
                    int user_ID_selected = Convert.ToInt32(readerID["user_ID"]);

                    if (!likedUser_ID.Contains(user_ID_selected) && user_ID_selected != user_ID)
                    {
                        if (!users.ContainsKey(user_ID_selected))
                        {
                            users.Add(user_ID_selected, 1);
                        }
                        else
                        {
                            users[user_ID_selected]++;
                        }
                    }
                }
                readerID.Close();
            }

            //testen of hij de goede gegevens ophaalt
            foreach (var user in users)
            {
                Debug.WriteLine("");
                Debug.WriteLine($"User ID: {user.Key}, Zelfde interesses: {user.Value}");
            }

            return users;

        }
        //code voor het halen van de foto uit de database
        public byte[] ShowUserPicture(int user_ID)
        {
            byte[] pictureByte = null;
            string queryPicture = "SELECT picture FROM profilepicture WHERE user_ID = @user_ID";

            try
            {
                var cmdPicture = new MySqlCommand(queryPicture, dbCon.Connection);
                cmdPicture.Parameters.AddWithValue("@user_ID", user_ID);
                var reader = cmdPicture.ExecuteReader();

                if (reader.Read())
                {
                    pictureByte = (byte[])reader["picture"];
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                // Handle exception as needed
                Console.WriteLine(ex.ToString());
            }

            return pictureByte;
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
                            Debug.WriteLine(reader.GetValue(i).ToString());
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
                return interests;
            }
        }

        // Retrieve list of interests from database using user_ID
        public List<List<string>> GetInterestsWithUserID(int user_ID)
        {
            List<List<string>> interests = new();
            try
            {
                if (dbCon.IsConnect())
                {
                    string query = "SELECT category, name FROM Interests WHERE interest_ID IN (SELECT interest_ID FROM userinterestlist WHERE user_ID = @us)";
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    cmd.Parameters.AddWithValue("@us", user_ID);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i += 2)
                        {
                            Debug.WriteLine($"category: {reader.GetValue(i)}");
                            Debug.WriteLine($"interest: {reader.GetValue(i + 1)}");
                            List<string> categoryInterestList = new();
                            categoryInterestList.Add(reader.GetValue(i).ToString());
                            categoryInterestList.Add(reader.GetValue(i + 1).ToString());
                            interests.Add(categoryInterestList);
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

        // Delete records from userinterestlist using user_ID
        public void DeleteFromUserInterestListWithUserID(int userID)
        {
            try
            {
                if (dbCon.IsConnect())
                {
                    string queryDelete = "DELETE FROM `userinterestlist` WHERE user_ID = @us";
                    var cmdDelete = new MySqlCommand(queryDelete, dbCon.Connection);
                    cmdDelete.Parameters.AddWithValue("@us", userID);
                    cmdDelete.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                dbCon.Close();
            }
        }

        //Bijwerken interesses van een bestaand account
        public void InsertIntoUserInterestList(int userID, List<string> interests)
        {
            try
            {
                if (dbCon.IsConnect())
                {
                    foreach (string interest in interests)
                    {
                        string querySelect = "SELECT interest_Id FROM Interests WHERE name = @in";
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
                        Debug.WriteLine($"{userID}, {interest_ID}");

                        string queryInsertInterest = "INSERT INTO `userinterestlist` (`user_ID`, `interest_ID`) VALUES (@us, @inID)";
                        var cmdInsertInterest = new MySqlCommand(queryInsertInterest, dbCon.Connection);
                        cmdInsertInterest.Parameters.AddWithValue("@us", userID);
                        cmdInsertInterest.Parameters.AddWithValue("@inID", interest_ID);
                        cmdInsertInterest.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                dbCon.Close();
            }
        }

        public void InsertIntoProfilePicture(int user_ID, Byte[] picture)
        {
            try
            {
                if (dbCon.IsConnect())
                {
                    string queryInsertPicture = "INSERT INTO `profilepicture` (`user_ID`, `picture`) VALUES (@us1, @pi)";
                    var cmdInsertPicture = new MySqlCommand(queryInsertPicture, dbCon.Connection);
                    cmdInsertPicture.Parameters.Add("@us1", MySqlDbType.VarChar, 100).Value = user_ID;
                    cmdInsertPicture.Parameters.Add("@pi", MySqlDbType.LongBlob).Value = picture;
                    cmdInsertPicture.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                dbCon.Close();
            }
        }

        public void InsertIntoLikesDislikes(int user_ID, int user_ID_Liked, string action)
        {
            try
            {
                if (dbCon.IsConnect())
                {
                    string queryInsertLike = "INSERT INTO `userLikesDislikes` (`user_ID`, `user_ID_Liked`, `action` ) VALUES (@usid, @usidlik, @ac)";
                    var cmdInsertLike = new MySqlCommand(queryInsertLike, dbCon.Connection);
                    cmdInsertLike.Parameters.AddWithValue("@usid", user_ID);
                    cmdInsertLike.Parameters.AddWithValue("@usidlik", user_ID_Liked);
                    cmdInsertLike.Parameters.AddWithValue("@ac", action);

                    cmdInsertLike.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                dbCon.Close();
            }
        }

        public bool CheckMatch(int user_ID, int user_ID_Liked)
        {
            bool checkMatch = false;
            string action;

            try
            {
                if (dbCon.IsConnect())
                {
                    string queryCheckMatch = $"SELECT action FROM userLikesDislikes WHERE user_ID = {user_ID_Liked} AND user_ID_Liked = {user_ID}" ;
                    var cmdCheckMatch = new MySqlCommand(queryCheckMatch, dbCon.Connection);                
                    var reader = cmdCheckMatch.ExecuteReader();
                    while (reader.Read())
                    {
                        action = reader.GetValue(0).ToString();
                        if (action.Equals("like"))
                        {
                            checkMatch = true;
                        }
                    }
                    reader.Close();
                }
                return checkMatch;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                dbCon.Close();
                return checkMatch;
            }
        }

        public void InsertMatch(int user_ID, int user_ID_Matched)
        {
            try
            {
                if (dbCon.IsConnect())
                {
                    string queryInsertMatch = $"INSERT INTO `usermatches` (`user_ID`, `user_ID_Matched`) VALUES ({user_ID}, {user_ID_Matched})";
                    var cmdInsertMatch = new MySqlCommand(queryInsertMatch, dbCon.Connection);

                    cmdInsertMatch.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                dbCon.Close();
            }
        }

        //Methode voor ophalen interesse ID
        public int SelectEventInterestID(string interest)
        {
            int interest_ID = Int32.MinValue;
            try
            {
                if (dbCon.IsConnect())
                {
                    string querySelect = "SELECT interest_Id FROM Interests WHERE name = @in";
                    var cmdSelect = new MySqlCommand(querySelect, dbCon.Connection);
                    cmdSelect.Parameters.AddWithValue("@in", interest);
                    var reader = cmdSelect.ExecuteReader();
                    while (reader.Read())
                    {
                        interest_ID = Int32.Parse(reader.GetValue(0).ToString());
                    }
                    reader.Close();
                    Debug.WriteLine(interest_ID);
                }
                return interest_ID;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                dbCon.Close();
                return interest_ID;
            }
        }

        //Methode voor ophalen interesse
        public string SelectEventInterestName(int interestID)
        {
            string interestName = null;
            try
            {
                if (dbCon.IsConnect())
                {
                    string querySelect = "SELECT name FROM Interests WHERE interest_Id = @id";
                    var cmdSelect = new MySqlCommand(querySelect, dbCon.Connection);
                    cmdSelect.Parameters.AddWithValue("@id", interestID);
                    var reader = cmdSelect.ExecuteReader();
                    while (reader.Read())
                    {
                        interestName = reader.GetString(0);
                    }
                    reader.Close();
                    Debug.WriteLine(interestName);
                }
                return interestName;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                dbCon.Close();
                return interestName;
            }
        }


        //Methode voor het aanmaken van een event
        public void InsertIntoEventsList(string eventName, int currentAttendees, int maxAttendees, string location, DateTime date, TimeOnly time, int interest_ID, int user_ID)
        {
            DateTime combinedDateTime = date + time.ToTimeSpan();
            currentAttendees = 1;
            try
            {
                if (dbCon.IsConnect())
                {
                    string query = "INSERT INTO `events` (`event_ID`,`eventName`,`currentAttendees`,`maxAttendees`,`location`,`date`,`interest_ID`,`user_ID`) VALUES (NULL, @ena, @cura, @maxa, @lo, @date, @iid, @uid);";
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    cmd.Parameters.Add("@ena", MySqlDbType.VarChar, 80).Value = eventName;
                    cmd.Parameters.Add("@cura", MySqlDbType.Int16, 4).Value = currentAttendees;
                    cmd.Parameters.Add("@maxa", MySqlDbType.Int16, 4).Value = maxAttendees;
                    cmd.Parameters.Add("@lo", MySqlDbType.VarChar, 80).Value = location;
                    cmd.Parameters.Add("@date", MySqlDbType.DateTime).Value = combinedDateTime;
                    cmd.Parameters.Add("@iid", MySqlDbType.Int32, 4).Value = interest_ID;
                    cmd.Parameters.Add("@uid", MySqlDbType.Int32, 4).Value = user_ID;
                    Console.WriteLine(cmd.ExecuteNonQuery());
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                dbCon.Close();
            }
        }

        //Methode voor annuleren(verwijderen) van een event
        public void DeleteFromEventsList(int event_ID, int user_ID)
        {
            try
            {
                if (dbCon.IsConnect())
                {
                    string query = "DELETE FROM `events` WHERE event_ID = @eid AND user_ID = @uid";
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    cmd.Parameters.AddWithValue("@eid", event_ID);
                    cmd.Parameters.AddWithValue("@uid", user_ID);
                    Console.WriteLine(cmd.ExecuteNonQuery());
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                dbCon.Close();
            }

        }

        //Methode voor weergave event informatie
        public Dictionary<string, string> ShowEventInformation(int event_ID)
        {
            Dictionary<string, string> keyValuePairsEvent = new();
            string query = "SELECT * FROM Events WHERE event_ID = @event_ID";
            try
            {
                if (dbCon.IsConnect())
                {
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    cmd.Parameters.AddWithValue("@event_ID", event_ID);
                    var reader = cmd.ExecuteReader();
                    string[] keys = new string[8] { "event_ID", "eventname", "currentattendees", "maxattendees", "location", "date", "interest_ID", "user_ID" };

                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write(reader.GetValue(i).ToString() + " ");
                            keyValuePairsEvent.Add(keys[i], reader.GetValue(i).ToString());
                        }
                        Console.WriteLine();
                    }
                    reader.Close();
                    return keyValuePairsEvent;
                }
                return keyValuePairsEvent;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return keyValuePairsEvent;
            }
        }

        //Methode Weergave alle event info?
        public List<Dictionary<string, string>> ShowAllEventInformation()
        {
            List<string> eventsIDList = new();
            List<Dictionary<string, string>> eventsInfoList = new();
            string query = "SELECT event_ID FROM Events";
            try
            {
                if (dbCon.IsConnect())
                {
                    var cmdEventID = new MySqlCommand(query, dbCon.Connection);
                    var reader = cmdEventID.ExecuteReader();

                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Debug.Write(reader.GetValue(i).ToString() + " ");
                            eventsIDList.Add(reader.GetValue(i).ToString());
                        }
                    }
                    reader.Close();

                    foreach (var id in eventsIDList)
                    {
                        int event_ID = int.Parse(id);
                        eventsInfoList.Add(ShowEventInformation(event_ID));
                    }
                    return eventsInfoList;
                }
                return eventsInfoList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return eventsInfoList;
            }
        }

        //Methode om je aan te melden voor een event
        public void InsertIntoUserEventsList(int event_ID, int user_ID)
        {
            try
            {
                if (dbCon.IsConnect())
                {
                    string query = "INSERT INTO `usereventlist` (`event_ID`, `user_ID`) VALUES (@evid, @usid)";
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    cmd.Parameters.AddWithValue("@evid", event_ID);
                    cmd.Parameters.AddWithValue("@usid", user_ID);
                    Console.WriteLine(cmd.ExecuteNonQuery());

                    UpdateCurrentAttendees(event_ID, "add");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                dbCon.Close();
            }

        }

        //Methode om je af te melden voor een event
        public void DeleteFromUserEventsList(int event_ID, int user_ID)
        {
            try
            {
                if (dbCon.IsConnect())
                {
                    string query = "DELETE FROM `usereventlist` WHERE event_ID = @evid AND user_ID = @usid";
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    cmd.Parameters.AddWithValue("@evid", event_ID);
                    cmd.Parameters.AddWithValue("@usid", user_ID);
                    Console.WriteLine(cmd.ExecuteNonQuery());

                    UpdateCurrentAttendees(event_ID , "delete");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                dbCon.Close();
            }

        }

        //Methode om currentAttendee waarde bij te werken
        public void UpdateCurrentAttendees(int event_ID, string calculation)
        {
            try
            {
                if (dbCon.IsConnect())
                {
                    if(calculation.Equals("add"))
                    {
                        string addQuery = "UPDATE Events SET totalAttendees = currentAttendees + 1  WHERE event_ID = @evid";
                        var addCmd = new MySqlCommand(addQuery, dbCon.Connection);
                        addCmd.Parameters.AddWithValue("@evid", event_ID);
                    }

                    if (calculation.Equals("delete"))
                    {
                        string delQuery = "UPDATE Events SET totalAttendees = currentAttendees - 1  WHERE event_ID = @evid";
                        var delCmd = new MySqlCommand(delQuery, dbCon.Connection);
                        delCmd.Parameters.AddWithValue("@evid", event_ID);
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                dbCon.Close();
            }

        }

        //Methode om te zien of een user zich heeft aangemeld
        public bool IsUserAttending(int event_ID, int user_ID)
        {

            try
            {
                if (dbCon.IsConnect())
                {
                    string query = "SELECT COUNT(*) > 0 AS user_attending FROM `usereventlist` WHERE event_ID = @evid AND user_ID = @usid";
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    cmd.Parameters.AddWithValue("@evid", event_ID);
                    cmd.Parameters.AddWithValue("@usid", user_ID);
                    Console.WriteLine(cmd.ExecuteNonQuery());

                   object result = cmd.ExecuteScalar();
                    return Convert.ToBoolean(result);
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                dbCon.Close();
                return false;
            }

        }

        //Account verwijderen
        public void DeleteUser(int userID)
        {
            List<string> tablesToDelete = new List<string>
                {                  
                    "account",
                    "events",
                    "usereventlist",
                    "profilepicture",
                    "userinterestlist",
                    "userlikesdislikes"
                };
            try
            {
                if (dbCon.IsConnect())
                {
                    foreach (string table in tablesToDelete)
                    {

                        try
                        {
                            string queryDeleteAll = $"DELETE FROM `{table}` WHERE user_ID = {userID}";
                            var cmdDeleteAll = new MySqlCommand(queryDeleteAll, dbCon.Connection);
                            cmdDeleteAll.ExecuteNonQuery();
                        }
                        catch
                        {
                            Debug.WriteLine($"{table} is niet gelukt a neef");
                        }
                    }

                    string queryDeleteLikes = $"DELETE FROM `userlikesdislikes` WHERE user_ID_Liked = {userID}";
                    var cmdDeleteLikes = new MySqlCommand(queryDeleteLikes, dbCon.Connection);
                    cmdDeleteLikes.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                dbCon.Close();
            }
        }

        public void ResetLikes(int user_ID)
        {
            try
            {
                if (dbCon.IsConnect())
                {
                    
                    string queryDeleteAll = $"DELETE FROM `userlikesdislikes` WHERE user_ID = {user_ID}";
                    var cmdDeleteAll = new MySqlCommand(queryDeleteAll, dbCon.Connection);
                    cmdDeleteAll.ExecuteNonQuery();                   

                }
                else
                {
                    Debug.WriteLine("Werkt niet kut");
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
