using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace DbConnectionLibrary.Tools
{
    public static class DbConnector
    {
        public static MySqlConnection GetConnection()
        {
            
            string connectionString = "Server=127.0.0.1;User=root;Password=1234;Database=my_sberbank_online_app";

            return new MySqlConnection(connectionString);
        }
    }
}
