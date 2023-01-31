using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CommunicationEntityClassLibrary;
using Newtonsoft.Json;

namespace ClientLibrary
{
    public class ClientManager
    {
        private HttpClient _client;

        public ClientManager()
        {
            _client = new HttpClient();
        }

        public async Task<Response> MakeRequestToServerAsync(Request request)
        {
            string requestData=JsonConvert.SerializeObject(request);

            var content=new StringContent(requestData, Encoding.UTF8,"application/json");

            HttpResponseMessage responseContext = await _client.PostAsync("http://localhost:12345/connection/",content);
        
        
            string responseData =await responseContext.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Response>(responseData);
        }

        
    }
}
