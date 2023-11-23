using Microsoft.Data.SqlClient;
using LinkApplication;

class Program
{
    static void Main()
    {

        // Connection details
        string ipAddress = "192.168.1.164:1433";
        string databaseName = "Matching";
        string username = "SA";
        string password = "@Matchingf3";

        // Create an instance of the DatabaseHandler class
        DatabaseConnecter dbHandler = new DatabaseConnecter(ipAddress, databaseName, username, password);

        // Open a database connection
        using (SqlConnection connection = dbHandler.OpenConnection())
        {
            // Connection is open, you can now execute SQL commands or queries here

            // Example: Execute a SQL command
            using (SqlCommand command = new SqlCommand("SELECT * FROM Account", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Process data here
                        Console.WriteLine(reader["ColumnName"]);
                    }
                }
            }
        }
    }
}
