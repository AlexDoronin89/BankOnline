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
    public class TableUserCards
    {
        public List<Card> GetUserCardsByIdUser(int idUser)
        {
            List<Card> userCards = new List<Card>();

            using (MySqlConnection connection = DbConnector.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $@"SELECT * FROM 'user_card' AS uc
                    JOIN 'card' AS c ON uc.id_card = c.id
                    WHERE ic.'id_user' = {idUser}";

                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        userCards.Add(new Card
                            (
                            reader.GetInt32("id"),
                            reader.GetInt32("number"),
                            reader.GetInt32("balance")
                            ));
                    }

                    reader.Close();

                }
                connection.Close();
            }
            return userCards;
        }

        public void AddUserCard(int idUser, Card card)
        {
            using(MySqlConnection connection = DbConnector.GetConnection())
            {
                connection.Open();
                using(MySqlTransaction transaction = connection.BeginTransaction())
                {
                    using(MySqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction=transaction;

                        try
                        {
                            command.CommandText = $"INSERT INTO 'card' ('number','balance') VALUES ({card.Number}, {card.Balance});";
                            command.ExecuteNonQuery();

                            command.CommandText = $"SELECT last_insert_id();";
                            int idCard = Convert.ToInt32(command.ExecuteScalar());

                            command.CommandText = $"INSERT INTO 'user_card' ('id_user','id_card') VALUES ({idUser}, {idCard});";
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
