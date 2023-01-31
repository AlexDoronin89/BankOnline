using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerLibrary;
using Newtonsoft.Json;
using System.Net;
using DbConnectionLibrary;
using BankEntityClassLibrary;
using CommunicationEntityClassLibrary;

namespace Server
{
    public class Program
    {
        private static Random _random = new Random();

        static void Main(string[] args)
        {
            Task.Factory.StartNew(() =>
            {
                ServerManager serverManager = new ServerManager();
                bool IsWork = true;

                while (IsWork)
                {
                    HttpListenerContext clientContext = serverManager.GetClientContext();
                    Task.Factory.StartNew(() => { ProccessClient(clientContext); });
                }
            });
        }

        private static void Log(string msg)
        {
            Console.WriteLine($"{DateTime.Now}:{msg}");
        }

        static void ProccessClient(HttpListenerContext clientContext)
        {
            DbManager db =new DbManager();

            Log("Client connected");

            Response response = null;
            Request request = null;

            Log($"Wait client");

            try
            {
                request = ServerManager.RecieveRequestFromClient(clientContext);
                Log($"Request from client:\n{request}");
            }
            catch (Exception ex)
            {
                response = new Response("ERROR", ex.Message);
                
            }

            if (request != null)
            {
                if (request.Token != "sjkJHGHJSDHJ732856kjhsdfgh8239shf")
                {
                    response = new Response("ERROR", "Wrong API key");
                }
                else
                {
                    response = ProcessClientCommand(request);
                }
            }

            try
            {
                Log($"Response to client:\n{response}");
                ServerManager.SendResponseToClient(clientContext);
            }
            catch(Exception ex)
            {
                Log("Connection with client error. Connection will be close");
                Log($"Error{ex.Message}");
            }

            

        }

        private static Response ProcessClientCommand (Request request)
        {
            DbManager db =new DbManager();

            Response response;

            try
            {
                switch (request.Command)
                {
                    case "users.select.by_login_and_password":
                            response = SelectUsers(request, db);
                            break;
                        
                    case "cards.insert.new":
                            response = CreateNewCard(request, db);
                            break;

                    case "cards.select.by_id_user":
                            response = SelectUserCards(request, db);
                            break;
                        
                    default:
                        response = new Response("ERROR", "Unknown command");
                        break;
                }
            }
            catch(Exception ex)
            {
                response = new Response("ERROR", ex.Message);
            }
            return response;
        }

        private static Response SelectUserCards(Request request,DbManager db)
        {
            Response response;
            int idUser = int.Parse(request.Parameters);

            List<Card> userCards=db.TableUserCards.GetUserCardsByIdUser(idUser);

            return new Response("OK",JsonConvert.SerializeObject(userCards));
        }

        private static Response CreateNewCard(Request request, DbManager db)
        {
            Response response;
            int idUser = int.Parse(request.Parameters);

            Card tempCard = null;
            int tempNumberCard;

            do
            {
                tempNumberCard=_random.Next(10000,100000);
                tempCard=db.TableCards.GetCardByNumber(tempNumberCard);
            }
            while(tempCard!=null);

            Card insertCard = new Card(0, 0, tempNumberCard);

            db.TableUserCards.AddUserCard(idUser, insertCard);
            return new Response("OK","true");
        }
        private static Response SelectUsers(Request request, DbManager db)
        {
            Response response;
            User requestUser=JsonConvert.DeserializeObject<User>(request.Parameters);

            User findUser = db.TableUsers.GetUserByLoginAndPassword(requestUser.Login,requestUser.Password);

            if (findUser == null)
            {
                return new Response("OK","Unknown user");
            }
            else
            {
            return new Response("OK", JsonConvert.SerializeObject(findUser));
            }
        }
    }
}
