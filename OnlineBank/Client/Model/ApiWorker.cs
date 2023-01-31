using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BankEntityClassLibrary;
using CommunicationEntityClassLibrary;
using ClientLibrary;

namespace Client.Model
{
    class ApiWorker
    {
        private static ApiWorker _instance = null;

        private ClientManager _clientManager;
        private string _token = "sjkJHGHJSDHJ732856kjhsdfgh8239shf";

        private ApiWorker()
        {
            _clientManager = new ClientManager();
        }

        public static ApiWorker GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ApiWorker();
            }

            return _instance;
        }

        public async Task<User> SelectUserByLoginAndPassword(string login, string password)
        {
            User requestUser = new User(0, string.Empty, login, password);
            return await MakeRequestToServerAsync<User>("users.select.by_login_and_password",JsonConvert.SerializeObject(requestUser),_token);
            //Request request = new Request("users.select.by_login_and_password", JsonConvert.SerializeObject(requestUser), _token);

            //Response response = await _clientManager.MakeRequestToServerAsync(request);

            //if (response.Status == "ERROR")
            //    throw new Exception(response.Message);

            //if (response.Status == "OK")
            //    return JsonConvert.DeserializeObject<User>(response.Message);

            //return null;
        }

        public async Task<bool> InsertNewCards(int idUser) =>
            await MakeRequestToServerAsync<bool>("cards.insert.new", idUser.ToString(), _token);
        //{
        //    //Request request = new Request("cards.insert.new", idUser.ToString(), _token);

        //    //Response response = await _clientManager.MakeRequestToServerAsync(request);

        //    //if (response.Status == "ERROR")
        //    //    throw new Exception(response.Message);

        //    //if (response.Status == "OK")
        //    //    return true;


        //    //return false;
        //}

        public async Task<List<Card>> SelectCardsByIdUser(int idUser) =>
            await MakeRequestToServerAsync<List<Card>>("cards.select.by_id_user", idUser.ToString(), _token);
        //{
        //    Request request = new Request("cards.select.by_id_user", idUser.ToString(), _token);

        //    Response response = await _clientManager.MakeRequestToServerAsync(request);

        //    if (response.Status == "ERROR")
        //        throw new Exception(response.Message);

        //    if (response.Status == "OK")
        //        return JsonConvert.DeserializeObject<List<Card>>(response.Message);

        //    return null;
        //}

        private async Task<T> MakeRequestToServerAsync<T>(string command, string parameters, string token)
        {
            Request request = new Request(command, parameters, token);

            Response response=await _clientManager.MakeRequestToServerAsync(request);

            if (response.Status == "ERROR")
                throw new Exception(response.Message);

            if (response.Status == "OK")
                return JsonConvert.DeserializeObject<T>(response.Message);

            return default;
        }

    }
}
