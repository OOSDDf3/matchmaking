using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace LinkApplication
{
    public class DatabaseConnecter
    {
        //Connectie maken met de database
        private readonly string connectionString;

        // Constructor to initialize the connection string
        public DatabaseConnecter(string ipAddress, string databaseName, string username, string password)
        {
            connectionString = $"Data Source={ipAddress};Initial Catalog={databaseName};User ID={username};Password={password};";
        }

        // Method to open a database connection
        public SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                Console.WriteLine("Connected to MSSQL Server");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return connection;
        }

        // Method to close a database connection
        public void CloseConnection(SqlConnection connection)
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    Console.WriteLine("Connection closed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        //Constructor waar je een query mee kan sturen
        //te gebruiken door andere methodes
    }
}
