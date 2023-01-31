using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbConnectionLibrary.Tools;
using MySql.Data.MySqlClient;
using BankEntityClassLibrary;

namespace DbConnectionLibrary.Tables
{
    public class TableUsers
    {
        public User GetUserByLoginAndPassword(string login, string password)
        {
            User user = null;

            using (MySqlConnection connection = DbConnector.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT * FROM 'user' WHERE 'login'='{login}' AND 'password'='{password}'";
                
                    MySqlDataReader reader=command.ExecuteReader();

                    if (reader.HasRows == true)
                    {
                        reader.Read();

                        user = new User(
                            reader.GetInt32("id"),
                            reader.GetString("name"),
                            reader.GetString("login"),
                            reader.GetString("password")
                            ) ;
                    }
                    reader.Close();
                }
                connection.Close();
            }

                return user;
        }
    }
}
