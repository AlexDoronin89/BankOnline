using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunicationEntityClassLibrary;
using MySql.Data.MySqlClient;
using DbConnectionLibrary.Tools;
using BankEntityClassLibrary;

namespace DbConnectionLibrary.Tables
{
    public class TableCards
    {
        public Card GetCardByNumber(int number)
        {
            Card card = null;

            using (MySqlConnection connection = DbConnector.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT * FROM 'cards' WHERE 'number'={number};";
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows == true)
                    {
                        reader.Read();

                        card = new Card(
                            id:reader.GetInt32("id"),
                            number:reader.GetInt32("number"),
                            balance:reader.GetInt32("balance")
                            );
                        
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return card;
        }

        public void SendMoneyFromCardToCard(int numberFrom, int numberTo,int money)
        {
            Card cardFrom = GetCardByNumber(numberFrom);

            if (cardFrom == null)
                throw new Exception("Incorrect card number");

            Card cardTo = GetCardByNumber(numberTo);

            if (cardTo == null)
                throw new Exception("Incorrect card number");

            if (cardFrom.Balance < money)
                throw new Exception("Dont have enough money");

            using (MySqlConnection connection = DbConnector.GetConnection())
            {
                connection.Open();
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    using (MySqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction = transaction;

                        try
                        {
                            command.CommandText = $"UPDATE 'cards' SET 'balance'='balance'-{money} WHERE 'number'={numberFrom};";
                            command.ExecuteNonQuery();

                            command.CommandText = $"UPDATE 'cards' SET 'balance'='balance'+{money} WHERE 'number'={numberTo};";
                            command.ExecuteNonQuery();

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }

                    }
                }
                connection.Close();
            }
        }

    }
}
