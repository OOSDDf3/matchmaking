using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkApplication
{
    public class Database_Connecter
    {
        SqlConnectionStringBuilder? sqlConnectionStringBuilder = null;

        SqlConnection sqlConnection;

        public Database_Connecter() { 
            ConnectToDatabase();
            TestDatabase("SELECT naam, email, adres FROM Matching");
        }

        protected void ConnectToDatabase()
        {

            sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            sqlConnectionStringBuilder.DataSource = "localhost";
            sqlConnectionStringBuilder.UserID = "SA";
            sqlConnectionStringBuilder.Password = "@Matchingf3";
            sqlConnectionStringBuilder.InitialCatalog = "Matching";

            sqlConnection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

            sqlConnection.Open();
            Console.WriteLine("Connection made");
            sqlConnection.Close();
        }

        private void TestDatabase(string query)
        {
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    Console.WriteLine("===================================");

                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    {
                        sqlConnection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"Name: {0}, E-mail: {1}, Address: {2}", reader.GetString(0), reader.GetString(1), reader.GetString(2));
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ReadLine();
        }
    }
}
