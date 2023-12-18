using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkApplication
{
    public class DatabaseConnection
    {
        private DatabaseConnection()
        {
        }

        public string Server { get; set; }
        public string Database { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public MySqlConnection Connection { get; set; }

        private static DatabaseConnection _instance;
        public static DatabaseConnection Instance()
        {
            if (_instance == null)
                _instance = new DatabaseConnection();
            return _instance;
        }

        public bool IsConnect()
        {
            if (Connection == null)
            {
                if (String.IsNullOrEmpty(Database))
                    return false;
                string connectionString = string.Format("Server={0}; database={1}; UID={2}; password={3}", Server, Database, UserName, Password);
                Connection = new MySqlConnection(connectionString);
                Connection.Open();
            }

            return true;
        }

        public void Close()
        {
            Connection.Close();
        }
    }
}

